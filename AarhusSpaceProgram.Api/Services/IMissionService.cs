using AarhusSpaceProgram.Api.Dtos.Missions;
namespace AarhusSpaceProgram.Api.Services;

public interface IMissionService
{
    Task<IEnumerable<MissionDto>> GetAllAsync();
    Task<MissionDetailsDto?> GetByIdAsync(int id);
    Task<IEnumerable<MissionOverviewDto>> GetOverviewAsync();
    Task<IEnumerable<MissionDto>> GetByTargetBodyAsync(string targetBodyName);
    Task<(bool Success, string? Error, MissionDto? Mission)> CreateAsync(CreateMissionDto dto);
    Task<(bool Success, string? Error)> UpdateAsync(int id, UpdateMissionDto dto);
    Task<bool> DeleteAsync(int id);

    Task<(bool Success, string? Error)> AssignAstronautAsync(int missionId, int astronautId);
    Task<(bool Success, string? Error)> RemoveAstronautAsync(int missionId, int astronautId);
    Task<(bool Success, string? Error)> AssignScientistAsync(int missionId, int scientistId);
    Task<(bool Success, string? Error)> RemoveScientistAsync(int missionId, int scientistId);
}
