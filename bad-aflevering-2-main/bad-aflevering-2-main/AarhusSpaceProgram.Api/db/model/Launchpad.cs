using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model;

    [Table("Launchpad")]
    public class Launchpad
    {
        [Key]
        public int LaunchPad_Id { get; set; }

        public string Location { get; set; }
        public string Status { get; set; }
        public double Max_Weight_Supported { get; set; }

        //Many to one
        public List<Mission> Missions { get; set; }
    }
    