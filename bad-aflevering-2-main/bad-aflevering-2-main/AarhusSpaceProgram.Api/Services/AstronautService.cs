using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;

namespace AarhusSpaceProgram.Api.Services;

public class AstronautService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public AstronautService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<AstronautDto>> GetAllAsync()
    {
        return await _context.Astronauts
            .Select(a => new AstronautDto
            {
                AstronautId =a.Astronaunt_Id,
                Name = a.Name,
                Rank = a.Rank,
                Paygrade = a.Paygrade,
                HoursSimulation = a.Hours_Simulation,
                HoursSpace = a.Hours_Space
            })
            .ToListAsync();
    }

    public async Task<AstronautDto?> GetByIdAsync(int id)
    {
        var astronaut = await _context.Astronauts
            .FirstOrDefaultAsync(a => a.Astronaunt_Id == id);

        if (astronaut == null)
        {
            return null;
        }

        return new AstronautDto
        {
                AstronautId = astronaut.Astronaunt_Id,
                Name = astronaut.Name,
                Rank = astronaut.Rank,
                Paygrade = astronaut.Paygrade,
                HoursSimulation = astronaut.Hours_Simulation,
                HoursSpace = astronaut.Hours_Space
        };
    }

    public async Task<AstronautDto?> CreateAsync(AstronautDto dto)
    {
        var astronaut = new Astronaut
        {
                Name = dto.Name,
                Rank = dto.Rank,
                Paygrade = dto.Paygrade,
                Hours_Simulation = dto.HoursSimulation,
                Hours_Space = dto.HoursSpace
        };

        _context.Astronauts.Add(astronaut);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(astronaut.Astronaunt_Id);
    }

    public async Task<AstronautDto?> UpdateAsync(int id, AstronautDto dto)
    {
        var astronaut = await _context.Astronauts
            .FirstOrDefaultAsync(a => a.Astronaunt_Id == id);

        if (astronaut == null)
        {
            return null;
        }

        astronaut.Name = dto.Name;
        astronaut.Rank = dto.Rank;
        astronaut.Paygrade = dto.Paygrade;
        astronaut.Hours_Simulation = dto.HoursSimulation;
        astronaut.Hours_Space = dto.HoursSpace;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var astronaut = await _context.Astronauts
            .FirstOrDefaultAsync(a => a.Astronaunt_Id == id);

        if (astronaut == null)
        {
            return false;
        }

        _context.Astronauts.Remove(astronaut);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<AstronautDto>> GetAstronautExperience()
    {
        return await _context.Astronauts
        .OrderByDescending(a => a.Hours_Space)
        .Select(a => new AstronautDto
        {
            Name = a.Name,
            HoursSpace = a.Hours_Space
        })
        .ToListAsync();
    }
}