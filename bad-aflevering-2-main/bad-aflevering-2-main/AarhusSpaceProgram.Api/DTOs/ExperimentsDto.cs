namespace AarhusSpaceProgram.Api.DTOs;

public class ExperimentsDTO
{
    public int Experiment_id{get; set;}
    public string name{get; set;}
    public string description{get; set;}
    public DateTime creation_date{get; set;}
    public int ScientistId { get; set; } 
    public int MissionID{get; set;}

}