using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Models;

public class CelestialBody
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty; // Planet, Moon, etc.

    public List<Mission> Missions { get; set; } = new();
}