using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Dtos.Launchpads;

public class CreateLaunchpadDto
{
    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}
