using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstronautsController : ControllerBase
{
    private readonly AstronautService _service;

    public AstronautsController(AstronautService service)
    {
        _service = service;
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AstronautDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("{id}")]
    public async Task<ActionResult<AstronautDto>> GetById(int id)
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
    public async Task<ActionResult<AstronautDto>> Create([FromBody] AstronautDto dto)
    {
        var created = await _service.CreateAsync(dto);

        if (created == null)
        {
            return BadRequest("Astronaut could not be created.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.AstronautId }, created);
    }

    [Authorize(Roles ="Manager")]
    [HttpPut("{id}")]
    public async Task<ActionResult<AstronautDto>> Update(int id, [FromBody] AstronautDto dto)
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

    [Authorize(Roles ="Manager,Astronaut")]
    [HttpGet("experience")]
    public async Task<IActionResult> GetAstronautExperience()
    {
        var result = await _service.GetAstronautExperience();
        return Ok(result);
    }
}