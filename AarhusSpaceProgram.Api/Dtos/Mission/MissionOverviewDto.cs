namespace AarhusSpaceProgram.Api.Dtos.Missions;

public class MissionOverviewDto
{
    public int Id { get; set; }
    public string MissionName { get; set; } = string.Empty;
    public DateOnly? LaunchDate { get; set; }
    public string? ManagerName { get; set; }
    public string? RocketModel { get; set; }
    public string? LaunchpadLocation { get; set; }
    public string? TargetCelestialBodyName { get; set; }
}
