namespace AarhusSpaceProgram.Api.Dtos.Rockets;

public class RocketDto
{
    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public double Weight { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
}
