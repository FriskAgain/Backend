using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model
{
    [Table("Rocket")]
    public class Rocket
{
    [Key]
    public int Rocket_Id { get; set; }

    public string Model { get; set; }
    public double Payload_capacity { get; set; }
    public int Crew_Capacity { get; set; }
    public double Fuel_Capacity { get; set; }
    public double Weight { get; set; }
    public int Stages{ get; set; }

    //One-one
    public Mission Mission { get; set; }
}
}