namespace AarhusSpaceProgram.Api.DTOs;

public class LaunchpadDto
{
    public int LaunchpadId { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    public double MaxWeightSupported { get; set; }
}