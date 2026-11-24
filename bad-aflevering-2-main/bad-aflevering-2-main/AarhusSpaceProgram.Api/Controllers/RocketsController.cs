using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RocketsController : ControllerBase
{
    private readonly RocketService _service;

    public RocketsController(RocketService service)
    {
        _service = service;
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RocketDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("{id}")]
    public async Task<ActionResult<RocketDto>> GetById(int id)
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
    public async Task<ActionResult<RocketDto>> Create([FromBody] RocketDto dto)
    {
        if(dto.Weight < 0)
        {
            return BadRequest("Weight can not be less than 0");
        }

        var created = await _service.CreateAsync(dto);

        if (created == null)
        {
            return BadRequest("Rocket could not be created.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.RocketId }, created);
    }

    [Authorize(Roles ="Manager")]
    [HttpPut("{id}")]
    public async Task<ActionResult<RocketDto>> Update(int id, [FromBody] RocketDto dto)
    {
        if(dto.Weight < 0)
        {
            return BadRequest("Weight can not be less than 0");
        }
        
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