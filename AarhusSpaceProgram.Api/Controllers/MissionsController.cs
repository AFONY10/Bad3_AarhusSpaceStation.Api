using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Dtos.Missions;
using AarhusSpaceProgram.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MissionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
    {
        var missions = await _context.Missions
            .Include(m => m.Astronauts)
            .Include(m => m.Scientists)
            .Select(m => new MissionDto
            {
                Id = m.Id,
                Name = m.Name,
                LaunchDate = m.LaunchDate,
                Status = m.Status.ToString(),
                ManagerId = m.ManagerId,
                RocketId = m.RocketId,
                LaunchpadId = m.LaunchpadId,
                TargetCelestialBodyId = m.TargetCelestialBodyId,
                AstronautIds = m.Astronauts.Select(a => a.Id).ToList(),
                ScientistIds = m.Scientists.Select(s => s.Id).ToList()
            })
            .ToListAsync();

        return Ok(missions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MissionDto>> GetMission(int id)
    {
        var m = await _context.Missions
            .Include(x => x.Astronauts)
            .Include(x => x.Scientists)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (m == null) return NotFound();

        var dto = new MissionDto
        {
            Id = m.Id,
            Name = m.Name,
            LaunchDate = m.LaunchDate,
            Status = m.Status.ToString(),
            ManagerId = m.ManagerId,
            RocketId = m.RocketId,
            LaunchpadId = m.LaunchpadId,
            TargetCelestialBodyId = m.TargetCelestialBodyId,
            AstronautIds = m.Astronauts.Select(a => a.Id).ToList(),
            ScientistIds = m.Scientists.Select(s => s.Id).ToList()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<MissionDto>> CreateMission(CreateMissionDto dto)
    {
        var mission = new Mission
        {
            Name = dto.Name,
            LaunchDate = dto.LaunchDate
        };

        if (!string.IsNullOrEmpty(dto.Status) && Enum.TryParse<MissionStatus>(dto.Status, true, out var parsedStatus))
        {
            mission.Status = parsedStatus;
        }

        if (dto.ManagerId.HasValue) mission.ManagerId = dto.ManagerId;
        if (dto.RocketId.HasValue) mission.RocketId = dto.RocketId;
        if (dto.LaunchpadId.HasValue) mission.LaunchpadId = dto.LaunchpadId;
        if (dto.TargetCelestialBodyId.HasValue) mission.TargetCelestialBodyId = dto.TargetCelestialBodyId;

        if (dto.AstronautIds != null && dto.AstronautIds.Any())
        {
            var astronauts = await _context.Astronauts.Where(a => dto.AstronautIds.Contains(a.Id)).ToListAsync();
            mission.Astronauts = astronauts;
        }

        if (dto.ScientistIds != null && dto.ScientistIds.Any())
        {
            var scientists = await _context.Scientists.Where(s => dto.ScientistIds.Contains(s.Id)).ToListAsync();
            mission.Scientists = scientists;
        }

        _context.Missions.Add(mission);
        await _context.SaveChangesAsync();

        var result = new MissionDto
        {
            Id = mission.Id,
            Name = mission.Name,
            LaunchDate = mission.LaunchDate,
            Status = mission.Status.ToString(),
            ManagerId = mission.ManagerId,
            RocketId = mission.RocketId,
            LaunchpadId = mission.LaunchpadId,
            TargetCelestialBodyId = mission.TargetCelestialBodyId,
            AstronautIds = mission.Astronauts.Select(a => a.Id).ToList(),
            ScientistIds = mission.Scientists.Select(s => s.Id).ToList()
        };

        return CreatedAtAction(nameof(GetMission), new { id = mission.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMission(int id, UpdateMissionDto dto)
    {
        var mission = await _context.Missions
            .Include(m => m.Astronauts)
            .Include(m => m.Scientists)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mission == null) return NotFound();

        mission.Name = dto.Name;
        mission.LaunchDate = dto.LaunchDate;

        if (!string.IsNullOrEmpty(dto.Status) && Enum.TryParse<MissionStatus>(dto.Status, true, out var parsedStatus))
        {
            mission.Status = parsedStatus;
        }

        mission.ManagerId = dto.ManagerId;
        mission.RocketId = dto.RocketId;
        mission.LaunchpadId = dto.LaunchpadId;
        mission.TargetCelestialBodyId = dto.TargetCelestialBodyId;

        // replace astronaut list
        mission.Astronauts.Clear();
        if (dto.AstronautIds != null && dto.AstronautIds.Any())
        {
            var astronauts = await _context.Astronauts.Where(a => dto.AstronautIds.Contains(a.Id)).ToListAsync();
            mission.Astronauts.AddRange(astronauts);
        }

        // replace scientist list
        mission.Scientists.Clear();
        if (dto.ScientistIds != null && dto.ScientistIds.Any())
        {
            var scientists = await _context.Scientists.Where(s => dto.ScientistIds.Contains(s.Id)).ToListAsync();
            mission.Scientists.AddRange(scientists);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMission(int id)
    {
        var mission = await _context.Missions.FindAsync(id);
        if (mission == null) return NotFound();

        _context.Missions.Remove(mission);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
