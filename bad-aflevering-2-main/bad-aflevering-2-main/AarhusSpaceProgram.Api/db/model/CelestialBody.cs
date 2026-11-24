using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model;

[Table("CelestialBody")]
public class CelestialBody
{
    [Key]
    public int Body_Id { get; set; }

    public string Name { get; set; }
    public string Body_Type { get; set; }
    public double Distance { get; set; }

    //Many to one
    public List<Mission> Missions { get; set; }
}
