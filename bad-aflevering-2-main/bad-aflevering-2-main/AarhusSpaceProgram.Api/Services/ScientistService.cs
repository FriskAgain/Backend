using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;

namespace AarhusSpaceProgram.Api.Services;

public class ScientistService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public ScientistService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<ScientistDto>> GetAllAsync()
    {
        return await _context.Scientists
            .Select(s => new ScientistDto
            {
                ScientistId = s.Scientist_Id,
                Name = s.Name,
                Title = s.Title,
                Specialty = s.Specialty,
                HireDate = s.HireDate
            })
            .ToListAsync();
    }

    public async Task<ScientistDto?> GetByIdAsync(int id)
    {
        var scientist = await _context.Scientists
            .FirstOrDefaultAsync(s => s.Scientist_Id == id);

        if (scientist == null)
        {
            return null;
        }

        return new ScientistDto
        {
                ScientistId = scientist.Scientist_Id,
                Name = scientist.Name,
                Title = scientist.Title,
                Specialty = scientist.Specialty,
                HireDate = scientist.HireDate
        };
    }

    public async Task<ScientistDto?> CreateAsync(ScientistDto dto)
    {
        var scientist = new Scientist
        {
                Name = dto.Name,
                Title = dto.Title,
                Specialty = dto.Specialty,
                HireDate = dto.HireDate
        };

        _context.Scientists.Add(scientist);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(scientist.Scientist_Id);
    }

    public async Task<ScientistDto?> UpdateAsync(int id, ScientistDto dto)
    {
        var scientist = await _context.Scientists
            .FirstOrDefaultAsync(s => s.Scientist_Id == id);

        if (scientist == null)
        {
            return null;
        }

        scientist.Name = dto.Name;
        scientist.Title = dto.Title;
        scientist.Specialty = dto.Specialty;
        scientist.HireDate = dto.HireDate;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var scientist = await _context.Scientists
            .FirstOrDefaultAsync(s => s.Scientist_Id == id);

        if (scientist == null)
        {
            return false;
        }

        _context.Scientists.Remove(scientist);
        await _context.SaveChangesAsync();
        return true;
    }
}