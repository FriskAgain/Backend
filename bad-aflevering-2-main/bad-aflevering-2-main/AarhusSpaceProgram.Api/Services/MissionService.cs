using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Enums;

namespace AarhusSpaceProgram.Api.Services;

public class MissionService
{
    private readonly AarhusSpaceProgramDbContext _context;

    public MissionService(AarhusSpaceProgramDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReadMissionDto>> GetAllAsync(MissionStatus? status = null)
    {
        var query = _context.Missions.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(m => m.Status == status.Value);
        }

        return await query
            .Select(m => new ReadMissionDto
            {
                MissionId = m.Mission_Id,
                Name = m.Name,
                LaunchDate = m.Launch_Date,
                PlannedDuration = m.PlannedDuration,
                Status = m.Status.ToString(),
                MissionType = m.Mission_Type,
                Experiments = m.Experiments.Select(e => e.name).ToList()
            })
            .ToListAsync();
    }

    public async Task<ReadMissionDto?> GetByIdAsync(int id)
    {
        var mission = await _context.Missions
            .Include(m => m.Experiments)
            .FirstOrDefaultAsync(m => m.Mission_Id == id);

        if (mission == null)
        {
            return null;
        }

        return new ReadMissionDto
        {
            MissionId = mission.Mission_Id,
            Name = mission.Name,
            LaunchDate = mission.Launch_Date,
            PlannedDuration = mission.PlannedDuration,
            Status = mission.Status.ToString(),
            MissionType = mission.Mission_Type,
            Experiments = mission.Experiments.Select(e => e.name).ToList()
        };
    }

    public async Task<ReadMissionDto?> CreateAsync(WriteMissionDto dto)
    {
        bool CantLaunch = await _context.Missions.AnyAsync(m =>
            m.Launchpad_Id == dto.LaunchpadId &&
            m.Launch_Date.Date == dto.LaunchDate.Date);

        if(CantLaunch)
        {
            return null;
        }

        if (dto.Status == MissionStatus.Planned || dto.Status == MissionStatus.Active)
        {
            return null;
        }

        var mission = new Mission
        {
            Name = dto.Name,
            Launch_Date = dto.LaunchDate,
            PlannedDuration = dto.PlannedDuration,
            Status = dto.Status,
            Mission_Type = dto.MissionType,
            Rocket_Id = dto.RocketId,
            Launchpad_Id = dto.LaunchpadId,
            Manager_Id = dto.ManagerId,
            CelestialBody_Id = dto.CelestialBodyId,
        };

        _context.Missions.Add(mission);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(mission.Mission_Id);
    }

    public async Task<ReadMissionDto?> UpdateAsync(int id, WriteMissionDto dto)
    {
        var mission = await _context.Missions
            .Include(m => m.MissionAstronauts)
            .Include(m => m.MissionScientists)
            .FirstOrDefaultAsync(m => m.Mission_Id == id);

        if (mission == null)
        {
            return null;
        }

        bool CantLaunch = await _context.Missions.AnyAsync(m =>
                m.Launchpad_Id == dto.LaunchpadId &&
                m.Launch_Date.Date == dto.LaunchDate.Date &&
                m.Mission_Id != id);

        if(CantLaunch)
        {
            return null;
        }

        if ((dto.Status == MissionStatus.Planned || dto.Status == MissionStatus.Active) &&
            (!mission.MissionAstronauts.Any() || !mission.MissionScientists.Any()))
        {
            return null;
        }

        mission.Name = dto.Name;
        mission.Launch_Date = dto.LaunchDate;
        mission.PlannedDuration = dto.PlannedDuration;
        mission.Status = dto.Status;
        mission.Mission_Type = dto.MissionType;
        mission.Rocket_Id = dto.RocketId;
        mission.Launchpad_Id = dto.LaunchpadId;
        mission.Manager_Id = dto.ManagerId;
        mission.CelestialBody_Id = dto.CelestialBodyId;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var mission = await _context.Missions
            .FirstOrDefaultAsync(m => m.Mission_Id == id);

        if (mission == null)
        {
            return false;
        }

        _context.Missions.Remove(mission);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<MissionOverviewDto>> GetOverviewAsync()
    {
        return await _context.Missions
            .Include(m => m.Manager)
            .Include(m => m.Rocket)
            .Include(m => m.Launchpad)
            .Include(m => m.Target)
            .Select(m => new MissionOverviewDto
            {
                Name = m.Name,
                LaunchDate = m.Launch_Date,
                ManagerName = m.Manager.Name,
                RocketModel = m.Rocket.Model,
                LaunchpadLocation = m.Launchpad.Location,
                TargetName = m.Target.Name
            })
            .ToListAsync();
    }

    public async Task<MissionDetailsDto?> GetDetailsAsync(int id)
    {
        var mission = await _context.Missions
            .Include(m => m.Manager)
            .Include(m => m.Rocket)
            .Include(m => m.Launchpad)
            .Include(m => m.Target)
            .Include(m => m.Experiments)
            .Include(m => m.MissionAstronauts)
                .ThenInclude(ma => ma.Astronaut)
            .Include(m => m.MissionScientists)
                .ThenInclude(ms => ms.Scientist)
            .FirstOrDefaultAsync(m => m.Mission_Id == id);

        if (mission == null)
        {  
            return null;
        }

        return new MissionDetailsDto
        {
            Name = mission.Name,
            LaunchDate = mission.Launch_Date.ToString(),
            PlannedDuration = mission.PlannedDuration,
            Status = mission.Status.ToString(),
            MissionType = mission.Mission_Type,
            ManagerName = mission.Manager.Name,
            LaunchpadLocation = mission.Launchpad.Location,
            TargetName = mission.Target.Name,
            Astronauts = mission.MissionAstronauts.Select(ma => ma.Astronaut.Name).ToList(),
            Scientists = mission.MissionScientists.Select(ms => ms.Scientist.Name).ToList(),
            Experiments = mission.Experiments.Select(e => e.name).ToList()
        };
    }

    public async Task<List<MissionTargetDto>> GetByTargetAsync(int targetId)
    {
        return await _context.Missions
            .Where(m => m.CelestialBody_Id == targetId)
            .Select(m => new MissionTargetDto
            {
                Name = m.Name,
                LaunchDate = m.Launch_Date,
                Status = m.Status.ToString()
            })
            .ToListAsync();
    }

    public async Task<bool> AssignAstronaut(int missionId, int astronautId)
    {
        var mission = await _context.Missions.FindAsync(missionId);
        var astronaut = await _context.Astronauts.FindAsync(astronautId);

        if (mission == null || astronaut == null)
            return false;

        var exists = await _context.MissionAstronauts
            .AnyAsync(ma => ma.MissionId == missionId && ma.AstronautId == astronautId);

        if (exists)
            return true;

        _context.MissionAstronauts.Add(new MissionAstronaut
        {
            MissionId = missionId,
            AstronautId = astronautId
        });

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveAstronaut(int missionId, int astronautId)
    {   
        var relation = await _context.MissionAstronauts
            .FirstOrDefaultAsync(ma => ma.MissionId == missionId && ma.AstronautId == astronautId);

        if (relation == null)
            return false;

        _context.MissionAstronauts.Remove(relation);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AssignScientist(int missionId, int scientistId)
    {
        var mission = await _context.Missions.FindAsync(missionId);
        var scientist = await _context.Scientists.FindAsync(scientistId);

        if (mission == null || scientist == null)
            return false;

        var exists = await _context.MissionScientists
            .AnyAsync(ms => ms.MissionId == missionId && ms.ScientistId == scientistId);

        if (exists)
            return true;

        _context.MissionScientists.Add(new MissionScientist
        {
            MissionId = missionId,
            ScientistId = scientistId
        });

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveScientist(int missionId, int scientistId)
    {   
        var relation = await _context.MissionScientists
            .FirstOrDefaultAsync(ms => ms.MissionId == missionId && ms.ScientistId == scientistId);

        if (relation == null)
            return false;

        _context.MissionScientists.Remove(relation);
        await _context.SaveChangesAsync();

        return true;
    }
}