using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Dtos.Managers;
using AarhusSpaceProgram.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ManagersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManagerDto>>> GetManagers()
    {
        var items = await _context.Managers
            .Select(m => new ManagerDto { Id = m.Id, FullName = m.FullName, Department = m.Department })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManagerDto>> GetManager(int id)
    {
        var m = await _context.Managers.FindAsync(id);
        if (m == null) return NotFound();

        return Ok(new ManagerDto { Id = m.Id, FullName = m.FullName, Department = m.Department });
    }

    [HttpPost]
    public async Task<ActionResult<ManagerDto>> CreateManager(CreateManagerDto dto)
    {
        var m = new Manager { FullName = dto.FullName, Department = dto.Department };
        _context.Managers.Add(m);
        await _context.SaveChangesAsync();

        var result = new ManagerDto { Id = m.Id, FullName = m.FullName, Department = m.Department };
        return CreatedAtAction(nameof(GetManager), new { id = m.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateManager(int id, UpdateManagerDto dto)
    {
        var m = await _context.Managers.FindAsync(id);
        if (m == null) return NotFound();

        m.FullName = dto.FullName;
        m.Department = dto.Department;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManager(int id)
    {
        var m = await _context.Managers.FindAsync(id);
        if (m == null) return NotFound();

        _context.Managers.Remove(m);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
