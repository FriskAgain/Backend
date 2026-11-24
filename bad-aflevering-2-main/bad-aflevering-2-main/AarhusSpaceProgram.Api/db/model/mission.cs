using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AarhusSpaceProgram.Enums;

namespace AarhusSpaceProgram.db.model
{   
    [Table("Mission")]
    public class Mission
    {
        [Key]
        public int Mission_Id { get; set; }

        public string Name { get; set; }
        public DateTime Launch_Date { get; set; }
        public int PlannedDuration { get; set; }

        public MissionStatus Status { get; set; }
        public string Mission_Type { get; set; }

        // Foreign Keys many to one

        public int Rocket_Id { get; set; }
        public Rocket Rocket { get; set; }

        public int Launchpad_Id { get; set; }
        public Launchpad Launchpad { get; set; }

        public int Manager_Id { get; set; }
        public Manager Manager { get; set; }

        public int CelestialBody_Id { get; set; }
        public CelestialBody Target { get; set; }

        // Many-to-many
        public List<MissionAstronaut> MissionAstronauts { get; set; }
        public List<MissionScientist> MissionScientists { get; set; }

        //One-Many
        public List<Experiments> Experiments { get; set; } = new();

    }
}