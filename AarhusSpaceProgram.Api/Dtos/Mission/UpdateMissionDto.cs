using System.ComponentModel.DataAnnotations;
using AarhusSpaceProgram.Api.Models;

namespace AarhusSpaceProgram.Api.Dtos.Missions;

public class UpdateMissionDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public DateOnly? LaunchDate { get; set; }

    public int? ManagerId { get; set; }
    public int? RocketId { get; set; }
    public int? LaunchpadId { get; set; }
    public int? TargetCelestialBodyId { get; set; }

    public MissionStatus Status { get; set; }
}
