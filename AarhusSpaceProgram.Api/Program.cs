using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e =>
                e.Properties.ContainsKey("RequestMethod") &&
                e.Properties.ContainsKey("RequestPath") &&
                e.Properties.ContainsKey("StatusCode"))
            .WriteTo.MongoDBBson(
                databaseUrl: $"{builder.Configuration["Serilog:MongoDbUrl"]}?connectTimeoutMS=2000&serverSelectionTimeoutMS=2000",
                collectionName: builder.Configuration["Serilog:MongoDbCollection"],
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information))
        .CreateLogger();

    Log.Information("Serilog configured with MongoDB sink");
}
catch (Exception ex)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();

    Log.Warning(ex, "Failed to configure MongoDB logging. Using console logging only.");
}

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi("v1");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMissionService, MissionService>();

var app = builder.Build();

app.MapOpenApi("/openapi/{documentName}.json");
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Aarhus Space Program API")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode}";

    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
        diagnosticContext.Set("RequestPath", httpContext.Request.Path);
        diagnosticContext.Set("StatusCode", httpContext.Response.StatusCode);
        diagnosticContext.Set("Timestamp", DateTime.UtcNow);
    };

    options.GetLevel = (httpContext, elapsed, ex) =>
    {
        var method = httpContext.Request.Method;
        if (method == "POST" || method == "PUT" || method == "DELETE")
        {
            return Serilog.Events.LogEventLevel.Information;
        }

        return Serilog.Events.LogEventLevel.Verbose;
    };
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}

app.Run();
