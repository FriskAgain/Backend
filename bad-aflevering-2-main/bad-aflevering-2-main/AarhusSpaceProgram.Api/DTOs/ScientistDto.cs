namespace AarhusSpaceProgram.Api.DTOs;

public class ScientistDto
{
    public int ScientistId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Specialty { get; set; }
    public DateTime HireDate { get; set; }
}