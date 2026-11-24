using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarhusSpaceProgram.db.model;

public class MissionAstronaut
{
    public int MissionId { get; set; }
    public Mission Mission { get; set; }

    public int AstronautId { get; set; }
    public Astronaut Astronaut { get; set; }
}
public class MissionScientist
{
    public int MissionId { get; set; }
    public Mission Mission { get; set; }

    public int ScientistId { get; set; }
    public Scientist Scientist { get; set; }
}

