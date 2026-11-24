using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model;

[Table("Experiments")]
public class Experiments
{
    [Key]
    public int Experiment_id{get; set;}
    public string name{get; set;}
    public string description{get; set;}
    public DateTime creation_date{get; set;}

    //One-Many
    public int Mission_id{get; set;}
    public Mission Mission{get; set;}

    public int ScientistId { get; set; }
    public Scientist Scientist { get; set; }
 
}