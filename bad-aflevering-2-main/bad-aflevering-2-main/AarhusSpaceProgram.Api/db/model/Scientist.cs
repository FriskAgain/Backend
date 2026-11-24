using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model
{
    
[Table("Scientist")]
public class Scientist
{
    [Key]
    public int Scientist_Id { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public DateTime HireDate { get; set; }

    //Many to many
    public List<MissionScientist> MissionScientists { get; set; }

    //Many to one
    public List<Experiments> Experiments { get; set; }
}
}