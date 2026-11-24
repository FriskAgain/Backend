using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;

namespace AarhusSpaceProgram.Api.Services;

public class LaunchpadService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public LaunchpadService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<LaunchpadDto>> GetAllAsync()
    {
        return await _context.Launchpads
            .Select(l => new LaunchpadDto
            {
                LaunchpadId = l.LaunchPad_Id,
                Location = l.Location,
                Status = l.Status,
                MaxWeightSupported = l.Max_Weight_Supported
            })
            .ToListAsync();
    }

    public async Task<LaunchpadDto?> GetByIdAsync(int id)
    {
        var launchpad = await _context.Launchpads
            .FirstOrDefaultAsync(l => l.LaunchPad_Id == id);

        if (launchpad == null)
        {
            return null;
        }

        return new LaunchpadDto
        {
                LaunchpadId = launchpad.LaunchPad_Id,
                Location = launchpad.Location,
                Status = launchpad.Status,
                MaxWeightSupported = launchpad.Max_Weight_Supported
        };
    }

    public async Task<LaunchpadDto?> CreateAsync(LaunchpadDto dto)
    {
        var launchpad = new Launchpad
        {
                Location = dto.Location,
                Status = dto.Status,
                Max_Weight_Supported = dto.MaxWeightSupported
        };

        _context.Launchpads.Add(launchpad);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(launchpad.LaunchPad_Id);
    }

    public async Task<LaunchpadDto?> UpdateAsync(int id, LaunchpadDto dto)
    {
        var launchpad = await _context.Launchpads
            .FirstOrDefaultAsync(l => l.LaunchPad_Id == id);

        if (launchpad == null)
        {
            return null;
        }

        launchpad.Location = dto.Location;
        launchpad.Status = dto.Status;
        launchpad.Max_Weight_Supported = dto.MaxWeightSupported;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var launchpad = await _context.Launchpads
            .FirstOrDefaultAsync(l => l.LaunchPad_Id == id);

        if (launchpad == null)
        {
            return false;
        }

        _context.Launchpads.Remove(launchpad);
        await _context.SaveChangesAsync();
        return true;
    }
}