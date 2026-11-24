using AarhusSpaceProgram.Enums;

namespace AarhusSpaceProgram.Api.DTOs;

public class WriteMissionDto
{
    public string Name { get; set; }
    public DateTime LaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    public MissionStatus Status { get; set; }
    public string MissionType { get; set; }
    public int RocketId { get; set; }
    public int LaunchpadId { get; set; }
    public int ManagerId { get; set; }
    public int CelestialBodyId { get; set; }
}