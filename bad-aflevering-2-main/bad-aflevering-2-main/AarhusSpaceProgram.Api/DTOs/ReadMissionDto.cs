namespace AarhusSpaceProgram.Api.DTOs;

public class ReadMissionDto
{
    public int MissionId { get; set; }
    public string Name { get; set; }
    public DateTime LaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    public string Status { get; set; }
    public string MissionType { get; set; }
    public List<string> Experiments{get; set; } = new();
}