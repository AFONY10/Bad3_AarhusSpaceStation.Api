using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Dtos.Scientists;
using AarhusSpaceProgram.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScientistsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ScientistsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScientistDto>>> GetScientists()
    {
        var items = await _context.Scientists
            .Select(s => new ScientistDto
            {
                Id = s.Id,
                FullName = s.FullName,
                FieldOfExpertise = s.FieldOfExpertise
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScientistDto>> GetScientist(int id)
    {
        var s = await _context.Scientists.FindAsync(id);

        if (s == null)
            return NotFound();

        return Ok(new ScientistDto { Id = s.Id, FullName = s.FullName, FieldOfExpertise = s.FieldOfExpertise });
    }

    [HttpPost]
    public async Task<ActionResult<ScientistDto>> CreateScientist(CreateScientistDto dto)
    {
        var s = new Scientist { FullName = dto.FullName, FieldOfExpertise = dto.FieldOfExpertise };
        _context.Scientists.Add(s);
        await _context.SaveChangesAsync();

        var result = new ScientistDto { Id = s.Id, FullName = s.FullName, FieldOfExpertise = s.FieldOfExpertise };
        return CreatedAtAction(nameof(GetScientist), new { id = s.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScientist(int id, UpdateScientistDto dto)
    {
        var s = await _context.Scientists.FindAsync(id);
        if (s == null)
            return NotFound();

        s.FullName = dto.FullName;
        s.FieldOfExpertise = dto.FieldOfExpertise;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScientist(int id)
    {
        var s = await _context.Scientists.FindAsync(id);
        if (s == null)
            return NotFound();

        _context.Scientists.Remove(s);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
