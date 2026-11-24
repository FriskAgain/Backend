using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.db.model;
using AarhusSpaceProgram.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace AarhusSpaceProgram.API.db;
    
 public class AarhusSpaceProgramDbContext : IdentityDbContext<ApiUser>
    {
        public AarhusSpaceProgramDbContext(DbContextOptions<AarhusSpaceProgramDbContext> options)
            : base(options)
        {
        }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Rocket> Rockets { get; set; }
    public DbSet<Launchpad> Launchpads { get; set; }
    public DbSet<CelestialBody> CelestialBodies { get; set; }
    public DbSet<Astronaut> Astronauts { get; set; }
    public DbSet<Scientist> Scientists { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<MissionAstronaut> MissionAstronauts { get; set; }
    public DbSet<MissionScientist> MissionScientists { get; set; }
    public DbSet<Experiments> Experiments {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Mission>().HasKey(m => m.Mission_Id);
        modelBuilder.Entity<Rocket>().HasKey(r => r.Rocket_Id);
        modelBuilder.Entity<Launchpad>().HasKey(l => l.LaunchPad_Id);
        modelBuilder.Entity<CelestialBody>().HasKey(c => c.Body_Id);
        modelBuilder.Entity<Astronaut>().HasKey(a => a.Astronaunt_Id);
        modelBuilder.Entity<Scientist>().HasKey(s => s.Scientist_Id);
        modelBuilder.Entity<Manager>().HasKey(m => m.Manager_Id);
        modelBuilder.Entity<Experiments>().HasKey(m => m.Experiment_id);

        modelBuilder.Entity<MissionAstronaut>()
            .HasKey(ma => new { ma.MissionId, ma.AstronautId });

        modelBuilder.Entity<MissionAstronaut>()
            .HasOne(ma => ma.Mission)
            .WithMany(m => m.MissionAstronauts)
            .HasForeignKey(ma => ma.MissionId);

        modelBuilder.Entity<MissionAstronaut>()
            .HasOne(ma => ma.Astronaut)
            .WithMany(a => a.MissionAstronauts)
            .HasForeignKey(ma => ma.AstronautId);

        modelBuilder.Entity<MissionScientist>()
            .HasKey(ms => new { ms.MissionId, ms.ScientistId });

        modelBuilder.Entity<MissionScientist>()
            .HasOne(ms => ms.Mission)
            .WithMany(m => m.MissionScientists)
            .HasForeignKey(ms => ms.MissionId);

        modelBuilder.Entity<MissionScientist>()
            .HasOne(ms => ms.Scientist)
            .WithMany(s => s.MissionScientists)
            .HasForeignKey(ms => ms.ScientistId);

        modelBuilder.Entity<Mission>()
            .HasOne(m => m.Rocket)
            .WithOne(r => r.Mission)
            .HasForeignKey<Mission>(m => m.Rocket_Id);

        modelBuilder.Entity<Mission>()
            .HasOne(m => m.Launchpad)
            .WithMany(l => l.Missions)
            .HasForeignKey(m => m.Launchpad_Id);

        modelBuilder.Entity<Mission>()
            .HasOne(m => m.Manager)
            .WithMany(man => man.Missions)
            .HasForeignKey(m => m.Manager_Id);

        modelBuilder.Entity<Mission>()
            .HasOne(m => m.Target)
            .WithMany(c => c.Missions)
            .HasForeignKey(m => m.CelestialBody_Id);
        
        modelBuilder.Entity<Mission>()
            .Property(m => m.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Experiments>()
            .HasOne(m => m.Mission)
            .WithMany(m => m.Experiments)
            .HasForeignKey(e => e.Mission_id);

        modelBuilder.Entity<Experiments>()
            .HasOne(e => e.Scientist)
            .WithMany(s => s.Experiments)
            .HasForeignKey(e => e.ScientistId);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rocket>().HasData(
            new Rocket
            {
                Rocket_Id = 1,
                Model = "Falcon A",
                Payload_capacity = 5000,
                Crew_Capacity = 4,
                Stages = 2,
                Fuel_Capacity = 120000,
                Weight = 12000
            },
            new Rocket
            {
                Rocket_Id = 2,
                Model = "Orion X",
                Payload_capacity = 7500,
                Crew_Capacity = 6,
                Stages = 3,
                Fuel_Capacity = 180000,
                Weight = 18000
            });

        modelBuilder.Entity<Launchpad>().HasData(
            new Launchpad
            {
                LaunchPad_Id = 1,
                Location = "Aarhus North",
                Status = "Available",
                Max_Weight_Supported = 20000
            },
            new Launchpad
            {
                LaunchPad_Id = 2,
                Location = "Aarhus South",
                Status = "Available",
                Max_Weight_Supported = 25000
            });

        modelBuilder.Entity<CelestialBody>().HasData(
            new CelestialBody
            {
                Body_Id = 1,
                Name = "Moon",
                Body_Type = "Moon",
                Distance = 384400
            },
            new CelestialBody
            {
                Body_Id = 2,
                Name = "Mars",
                Body_Type = "Planet",
                Distance = 225000000
            },
            new CelestialBody
            {
                Body_Id = 3,
                Name = "Europa",
                Body_Type = "Moon",
                Distance = 628300000
            });

        modelBuilder.Entity<Manager>().HasData(
            new Manager
            {
                Manager_Id = 1,
                Name = "Maja Jensen",
                Department = "Mission Operations",
                Hire_Date = new DateTime(2021, 3, 1)
            },
            new Manager
            {
                Manager_Id = 2,
                Name = "Lucas Nielsen",
                Department = "Flight Planning",
                Hire_Date = new DateTime(2020, 8, 15)
            });

        modelBuilder.Entity<Astronaut>().HasData(
            new Astronaut
            {
                Astronaunt_Id = 1,
                Name = "Anna Holm",
                Rank = "Commander",
                Paygrade = "A1",
                Hours_Space = 420,
                Hours_Simulation = 900
            },
            new Astronaut
            {
                Astronaunt_Id = 2,
                Name = "Jonas Madsen",
                Rank = "Pilot",
                Paygrade = "A2",
                Hours_Space = 860,
                Hours_Simulation = 1100
            },
            new Astronaut
            {
                Astronaunt_Id = 3,
                Name = "Sara Poulsen",
                Rank = "Mission Specialist",
                Paygrade = "A2",
                Hours_Space = 210,
                Hours_Simulation = 700
            });

        modelBuilder.Entity<Scientist>().HasData(
            new Scientist
            {
                Scientist_Id = 1,
                Name = "Eva Larsen",
                Title = "Dr.",
                Specialty = "Astrobiology",
                HireDate = new DateTime(2022, 1, 10)
            },
            new Scientist
            {
                Scientist_Id = 2,
                Name = "Noah Berg",
                Title = "Dr.",
                Specialty = "Planetary Geology",
                HireDate = new DateTime(2021, 11, 5)
            });

        modelBuilder.Entity<Mission>().HasData(
            new Mission
            {
                Mission_Id = 1,
                Name = "Luna Survey One",
                Launch_Date = new DateTime(2026, 10, 15),
                PlannedDuration = 14,
                Status = MissionStatus.Planned,
                Mission_Type = "Research",
                Rocket_Id = 1,
                Launchpad_Id = 1,
                Manager_Id = 1,
                CelestialBody_Id = 1
            },
            new Mission
            {
                Mission_Id = 2,
                Name = "Mars Ice Mapper",
                Launch_Date = new DateTime(2026, 11, 20),
                PlannedDuration = 180,
                Status = MissionStatus.Created,
                Mission_Type = "Exploration",
                Rocket_Id = 2,
                Launchpad_Id = 2,
                Manager_Id = 2,
                CelestialBody_Id = 2
            });

            modelBuilder.Entity<Experiments>().HasData(
                new Experiments
                {
                    Experiment_id = 1,
                    name = "TestExperiment",
                    description="This is a test",
                    creation_date = new DateTime(2026,02,05),
                    Mission_id = 1,
                    ScientistId = 1
                });

        modelBuilder.Entity<MissionAstronaut>().HasData(
            new MissionAstronaut { MissionId = 1, AstronautId = 1 },
            new MissionAstronaut { MissionId = 1, AstronautId = 2 },
            new MissionAstronaut { MissionId = 2, AstronautId = 3 });

        modelBuilder.Entity<MissionScientist>().HasData(
            new MissionScientist { MissionId = 1, ScientistId = 1 },
            new MissionScientist { MissionId = 2, ScientistId = 2 });
    }
}