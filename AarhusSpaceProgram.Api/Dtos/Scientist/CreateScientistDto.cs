using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Dtos.Scientists;

public class CreateScientistDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FieldOfExpertise { get; set; } = string.Empty;
}
