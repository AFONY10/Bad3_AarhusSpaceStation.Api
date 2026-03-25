using System.Collections.Generic;

namespace AarhusSpaceProgram.Api.Dtos.Missions;

public class MissionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly? LaunchDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
    public int? RocketId { get; set; }
    public int? LaunchpadId { get; set; }
    public int? TargetCelestialBodyId { get; set; }
    public List<int> AstronautIds { get; set; } = new();
    public List<int> ScientistIds { get; set; } = new();
}
