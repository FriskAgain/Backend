using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;

namespace AarhusSpaceProgram.Api.Services;

public class ManagerService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public ManagerService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<ManagerDto>> GetAllAsync()
    {
        return await _context.Managers
            .Select(m => new ManagerDto
            {
                ManagerId = m.Manager_Id,
                Name = m.Name,
                Department = m.Department,
                HireDate = m.Hire_Date
            })
            .ToListAsync();
    }

    public async Task<ManagerDto?> GetByIdAsync(int id)
    {
        var manager = await _context.Managers
            .FirstOrDefaultAsync(m => m.Manager_Id == id);

        if (manager == null)
        {
            return null;
        }

        return new ManagerDto
        {
                ManagerId = manager.Manager_Id,
                Name = manager.Name,
                Department = manager.Department,
                HireDate = manager.Hire_Date
        };
    }

    public async Task<ManagerDto?> CreateAsync(ManagerDto dto)
    {
        var manager = new Manager
        {
                Name = dto.Name,
                Department = dto.Department,
                Hire_Date = dto.HireDate
        };

        _context.Managers.Add(manager);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(manager.Manager_Id);
    }

    public async Task<ManagerDto?> UpdateAsync(int id, ManagerDto dto)
    {
        var manager = await _context.Managers
            .FirstOrDefaultAsync(m => m.Manager_Id == id);

        if (manager == null)
        {
            return null;
        }

        manager.Name = dto.Name;
        manager.Department = dto.Department;
        manager.Hire_Date = dto.HireDate;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var manager = await _context.Managers
            .FirstOrDefaultAsync(m => m.Manager_Id == id);

        if (manager == null)
        {
            return false;
        }

        _context.Managers.Remove(manager);
        await _context.SaveChangesAsync();
        return true;
    }
}