namespace AarhusSpaceProgram.Api.DTOs;

public class AstronautDto
{
    public int AstronautId { get; set; }
    public string Name { get; set; }
    public string Rank { get; set; }
    public string Paygrade { get; set; }
    public int HoursSimulation { get; set; }
    public int HoursSpace { get; set; }
}