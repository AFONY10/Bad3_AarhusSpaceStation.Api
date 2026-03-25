using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Dtos.Missions;

public class CreateMissionDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public DateOnly? LaunchDate { get; set; }

    public string? Status { get; set; }

    public int? ManagerId { get; set; }
    public int? RocketId { get; set; }
    public int? LaunchpadId { get; set; }
    public int? TargetCelestialBodyId { get; set; }

    public List<int>? AstronautIds { get; set; } = new();
    public List<int>? ScientistIds { get; set; } = new();
}
