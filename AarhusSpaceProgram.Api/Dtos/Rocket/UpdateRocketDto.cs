using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Dtos.Rockets;

public class UpdateRocketDto
{
    [Required]
    [MaxLength(100)]
    public string Model { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public double Weight { get; set; }

    [Required]
    [MaxLength(100)]
    public string Manufacturer { get; set; } = string.Empty;
}
