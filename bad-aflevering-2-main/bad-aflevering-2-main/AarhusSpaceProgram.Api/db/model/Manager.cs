using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model
{
  [Table("Manager")]
  public class Manager
{
    [Key]
    public int Manager_Id { get; set; }

    public string Name { get; set; }
    public string Department { get; set; }
    public DateTime Hire_Date { get; set; }

    //Many to one
    public List<Mission> Missions { get; set; }
}
}