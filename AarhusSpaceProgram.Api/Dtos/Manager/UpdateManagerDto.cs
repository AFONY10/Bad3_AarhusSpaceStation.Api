using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Dtos.Managers;

public class UpdateManagerDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;
}
