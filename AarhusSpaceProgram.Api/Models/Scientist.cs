using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Models;

public class Scientist
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FieldOfExpertise { get; set; } = string.Empty;

    public List<Mission> Missions { get; set; } = new();
}