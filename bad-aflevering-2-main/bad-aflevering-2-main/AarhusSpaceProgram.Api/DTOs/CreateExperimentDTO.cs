using System.ComponentModel;

namespace AarhusSpaceProgram.Api.DTOs;

public class CreateExperimentDTO
{
    public string Name{get; set;}
    public string Description{get; set;}
    public int MissionID{get; set;}
    public int ScientistId{get; set;}
}