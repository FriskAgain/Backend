using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagersController : ControllerBase
{
    private readonly ManagerService _service;

    public ManagersController(ManagerService service)
    {
        _service = service;
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManagerDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ManagerDto>> GetById(int id)
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
    public async Task<ActionResult<ManagerDto>> Create([FromBody] ManagerDto dto)
    {
        var created = await _service.CreateAsync(dto);

        if (created == null)
        {
            return BadRequest("Manager could not be created.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.ManagerId }, created);
    }

    [Authorize(Roles ="Manager")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ManagerDto>> Update(int id, [FromBody] ManagerDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (updated == null)
        {
            return NotFound();
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
}