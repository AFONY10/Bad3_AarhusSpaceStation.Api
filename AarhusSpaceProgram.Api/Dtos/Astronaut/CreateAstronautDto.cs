using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Dtos.Astronauts;

public class CreateAstronautDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int HoursInSpace { get; set; }
}