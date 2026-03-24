using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.Models;

public class Launchpad
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public List<Mission> Missions { get; set; } = new();
}