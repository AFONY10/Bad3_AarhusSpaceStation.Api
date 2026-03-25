namespace AarhusSpaceProgram.Api.Dtos.Astronauts;

public class AstronautDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int HoursInSpace { get; set; }
}