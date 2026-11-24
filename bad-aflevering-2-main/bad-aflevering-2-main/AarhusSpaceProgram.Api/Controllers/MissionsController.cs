using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Services;
using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Enums;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AarhusSpaceProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    private readonly MissionService _service;
    private readonly MissionLogService _missionLogService;

    public MissionsController(MissionService service, MissionLogService missionLogService)
    {
        _service = service;
        _missionLogService = missionLogService;
    }

    //Everyone can access
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadMissionDto>>> GetAll([FromQuery] MissionStatus? status)
    {
        var result = await _service.GetAllAsync(status);
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadMissionDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [Authorize(Roles ="Manager")]
    [HttpPost]
    public async Task<ActionResult<ReadMissionDto>> Create([FromBody] WriteMissionDto dto)
    {
        var created = await _service.CreateAsync(dto);

        if (created == null)
        {
            return BadRequest("Mission could not be created.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.MissionId }, created);
    }

    [Authorize(Roles ="Manager")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReadMissionDto>> Update(int id, [FromBody] WriteMissionDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (updated == null)
        {
            return BadRequest("Mission could not be updated.");
        }

        return Ok(updated);
    }

    [Authorize(Roles ="Manager")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if(!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("overview")]
    public async Task<ActionResult<IEnumerable<MissionOverviewDto>>> GetOverview()
    {
        var result = await _service.GetOverviewAsync();
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("{id}/details")]
    public async Task<ActionResult<MissionDetailsDto>> GetDetails(int id)
    {
        var result = await _service.GetDetailsAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id}/logs")]
    public async Task<ActionResult<IEnumerable<MissionLogDto>>> GetLogsById(int id)
    {
        var result = await _missionLogService.GetLogsByMissionId(id);
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("by-target/{targetId}")]
    public async Task<ActionResult<IEnumerable<MissionTargetDto>>> GetByTarget(int targetId)
    {
        var result = await _service.GetByTargetAsync(targetId);
        return Ok(result);
    }

    [Authorize(Roles ="Manager")]
    [HttpPost("{missionId}/assign-astronaut/{astronautId}")]
    public async Task<IActionResult> AssignAstronaut(int missionId, int astronautId)
    {
        var result = await _service.AssignAstronaut(missionId, astronautId);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [Authorize(Roles ="Manager")]
    [HttpDelete("{missionId}/remove-astronaut/{astronautId}")]
    public async Task<IActionResult> RemoveAstronaut(int missionId, int astronautId)
    {
        var result = await _service.RemoveAstronaut(missionId, astronautId);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [Authorize(Roles ="Manager")]
    [HttpPost("{missionId}/assign-scientist/{scientistId}")]
    public async Task<IActionResult> AssignScientist(int missionId, int scientistId)
    {
        var result = await _service.AssignScientist(missionId, scientistId);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [Authorize(Roles ="Manager")]
    [HttpDelete("{missionId}/remove-scientist/{scientistId}")]
    public async Task<IActionResult> RemoveScientist(int missionId, int scientistId)
    {
        var result = await _service.RemoveScientist(missionId, scientistId);

        if (!result)
            return NotFound();

        return NoContent();
    }
}