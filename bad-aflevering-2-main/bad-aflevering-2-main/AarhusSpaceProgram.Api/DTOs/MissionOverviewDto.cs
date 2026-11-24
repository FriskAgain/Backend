namespace AarhusSpaceProgram.Api.DTOs;

public class MissionOverviewDto
{
    public string Name { get; set; }
    public DateTime LaunchDate { get; set; }
    public string ManagerName { get; set; }
    public string RocketModel { get; set; }
    public string LaunchpadLocation { get; set; }
    public string TargetName { get; set; }
}