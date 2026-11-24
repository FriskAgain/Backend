using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ExperimentController : ControllerBase
{
    private readonly ExperimentService _service;

    public ExperimentController(ExperimentService service)
    {
        _service = service;
    }

    [Authorize(Roles ="Manager,Astronaut,Scientist")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperimentsDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles ="Manager,Astronaut,Scientist")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ExperimentsDTO>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [Authorize(Roles ="Manager,Scientist")]
    [HttpPost]
    public async Task<ActionResult<ExperimentsDTO>> Create([FromBody] CreateExperimentDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        if(created == null)
        {
            return BadRequest("Experiment could not be created");
        }
        return CreatedAtAction(nameof(GetById), new {id=created.Experiment_id}, created);
    } 

    [Authorize(Roles ="Manager,Scientist")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ExperimentsDTO>>Update(int id, [FromBody]ExperimentsDTO dto)
    {
        var update = await _service.UpdateAsync(id, dto);
        if(update == null)
        {
            return NotFound();
        }

        return Ok(update);
    }

    [Authorize(Roles ="Manager,Scientist")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ExperimentsDTO>>Delete(int id)
    {
        var delete = await _service.Delete(id);
        if(delete == false)
        {
            return NotFound();
        }

        return Ok("Deleted");
    }
}
