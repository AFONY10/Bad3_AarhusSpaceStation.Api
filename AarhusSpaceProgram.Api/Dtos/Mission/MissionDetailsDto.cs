using System.Collections.Generic;
using AarhusSpaceProgram.Api.Models;

namespace AarhusSpaceProgram.Api.Dtos.Missions;

public class MissionDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly? LaunchDate { get; set; }
    public MissionStatus Status { get; set; }

    public string? ManagerName { get; set; }
    public string? RocketModel { get; set; }
    public string? LaunchpadLocation { get; set; }
    public string? TargetCelestialBodyName { get; set; }

    public List<string> Astronauts { get; set; } = new();
    public List<string> Scientists { get; set; } = new();
}
