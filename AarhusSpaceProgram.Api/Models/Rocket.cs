using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Models;

public class Rocket
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Model { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Rocket weight cannot be negative.")]
    public double Weight { get; set; }

    [MaxLength(100)]
    public string Manufacturer { get; set; } = string.Empty;

    public List<Mission> Missions { get; set; } = new();
}