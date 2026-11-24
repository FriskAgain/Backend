using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace AarhusSpaceProgram.Api.Services;

public class ExperimentService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public ExperimentService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExperimentsDTO>> GetAllAsync()
    {
        return await _context.Experiments
            .Select(e => new ExperimentsDTO
            {
                Experiment_id = e.Experiment_id,
                name = e.name,
                description = e.description,
                creation_date = e.creation_date,
                ScientistId=e.ScientistId,
                MissionID = e.Mission_id
            }).ToListAsync();
    }

    public async Task<ExperimentsDTO?> GetByIdAsync(int id)
    {
        var experiment = await _context.Experiments
            .FirstOrDefaultAsync(e => e.Experiment_id == id);

        if(experiment == null)
        {
            return null;
        }

        return new ExperimentsDTO
        {
                Experiment_id = experiment.Experiment_id,
                name = experiment.name,
                description = experiment.description,
                creation_date = experiment.creation_date,
                ScientistId = experiment.ScientistId,
                MissionID = experiment.Mission_id                
        };
    }

    public async Task<ExperimentsDTO> CreateAsync(CreateExperimentDTO dto)
    {
        var experiment = new Experiments
        {
            name = dto.Name,
            description = dto.Description,
            creation_date = DateTime.UtcNow,
            Mission_id = dto.MissionID,
            ScientistId = dto.ScientistId
        };

        _context.Experiments.Add(experiment);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(experiment.Experiment_id);
    }

    public async Task<ExperimentsDTO?> UpdateAsync(int id, ExperimentsDTO dto)
    {
        var experiment = await _context.Experiments
            .FirstOrDefaultAsync(e => e.Experiment_id == id);

        if(experiment == null)
        {
            return null;
        }

        experiment.name = dto.name;
        experiment.description = dto.description;
        experiment.creation_date = dto.creation_date;
        experiment.ScientistId = dto.ScientistId;
        experiment.Mission_id = dto.MissionID;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool>Delete(int id)
    {
        var experiment = await _context.Experiments
            .FirstOrDefaultAsync(e => e.Experiment_id==id);
        
        if(experiment == null)
        {
            return false;
        }

        _context.Remove(experiment);
        await _context.SaveChangesAsync();
        return true;
    }
}