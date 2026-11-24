using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model;

[Table("Astronaut")]
public class Astronaut
{
    [Key]
    public int Astronaunt_Id { get; set; }

    public string Name { get; set; }
    public string Rank { get; set; }
    public string Paygrade { get; set; }

    public int Hours_Simulation { get; set; }
    public int Hours_Space { get; set; }

    //Many to many
    public List<MissionAstronaut> MissionAstronauts { get; set; }
}
