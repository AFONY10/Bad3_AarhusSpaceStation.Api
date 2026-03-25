using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using AarhusSpaceProgram.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}

// Map OpenAPI JSON endpoint at /openapi/v1.json
app.MapOpenApi("/openapi/{documentName}.json");

// Map Scalar UI at /scalar
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Aarhus Space Program API")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();