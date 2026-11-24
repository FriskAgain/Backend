using AarhusSpaceProgram.db.model;

namespace AarhusSpaceProgram.Api.DTOs;

public class MissionDetailsDto
{
    public string Name { get; set; }
    public string LaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    public string Status { get; set; }
    public string MissionType { get; set; }
    public string ManagerName { get; set; }
    public string LaunchpadLocation { get; set; }
    public string TargetName { get; set; }
    public List<string> Astronauts { get; set; } = new();
    public List<string> Scientists { get; set; } = new();
    public List<string> Experiments{get; set; } = new();
}