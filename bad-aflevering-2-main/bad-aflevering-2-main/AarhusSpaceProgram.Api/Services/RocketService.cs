using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;

namespace AarhusSpaceProgram.Api.Services;

public class RocketService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public RocketService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<RocketDto>> GetAllAsync()
    {
        return await _context.Rockets
            .Select(r => new RocketDto
            {
                RocketId = r.Rocket_Id,
                Model = r.Model,
                PayloadCapacity = r.Payload_capacity,
                CrewCapacity = r.Crew_Capacity,
                FuelCapacity = r.Fuel_Capacity,
                Weight = r.Weight,
                Stages = r.Stages
            })
            .ToListAsync();
    }

    public async Task<RocketDto?> GetByIdAsync(int id)
    {
        var rocket = await _context.Rockets
            .FirstOrDefaultAsync(r => r.Rocket_Id == id);

        if (rocket == null)
        {
            return null;
        }

        return new RocketDto
        {
                RocketId = rocket.Rocket_Id,
                Model = rocket.Model,
                PayloadCapacity = rocket.Payload_capacity,
                CrewCapacity = rocket.Crew_Capacity,
                FuelCapacity = rocket.Fuel_Capacity,
                Weight = rocket.Weight,
                Stages = rocket.Stages
        };
    }

    public async Task<RocketDto?> CreateAsync(RocketDto dto)
    {
        var rocket = new Rocket
        {
                Model = dto.Model,
                Payload_capacity = dto.PayloadCapacity,
                Crew_Capacity = dto.CrewCapacity,
                Fuel_Capacity = dto.FuelCapacity,
                Weight = dto.Weight, 
                Stages = dto.Stages
        };

        _context.Rockets.Add(rocket);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(rocket.Rocket_Id);
    }

    public async Task<RocketDto?> UpdateAsync(int id, RocketDto dto)
    {
        var rocket = await _context.Rockets
            .FirstOrDefaultAsync(r => r.Rocket_Id == id);

        if (rocket == null)
        {
            return null;
        }

        rocket.Model = dto.Model;
        rocket.Payload_capacity = dto.PayloadCapacity;
        rocket.Crew_Capacity = dto.CrewCapacity;
        rocket.Fuel_Capacity = dto.FuelCapacity;
        rocket.Weight = dto.Weight; 
        rocket.Stages = dto.Stages;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rocket = await _context.Rockets
            .FirstOrDefaultAsync(r => r.Rocket_Id == id);

        if (rocket == null)
        {
            return false;
        }

        _context.Rockets.Remove(rocket);
        await _context.SaveChangesAsync();
        return true;
    }
}