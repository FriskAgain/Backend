using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AarhusSpaceProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class AarhusSpaceProgramDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Astronaut",
                columns: table => new
                {
                    Astronaunt_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paygrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours_Simulation = table.Column<int>(type: "int", nullable: false),
                    Hours_Space = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astronaut", x => x.Astronaunt_Id);
                });

            migrationBuilder.CreateTable(
                name: "CelestialBody",
                columns: table => new
                {
                    Body_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelestialBody", x => x.Body_Id);
                });

            migrationBuilder.CreateTable(
                name: "Launchpad",
                columns: table => new
                {
                    LaunchPad_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Max_Weight_Supported = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Launchpad", x => x.LaunchPad_Id);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Manager_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hire_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Manager_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rocket",
                columns: table => new
                {
                    Rocket_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payload_capacity = table.Column<double>(type: "float", nullable: false),
                    Crew_Capacity = table.Column<int>(type: "int", nullable: false),
                    Fuel_Capacity = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Stages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rocket", x => x.Rocket_Id);
                });

            migrationBuilder.CreateTable(
                name: "Scientist",
                columns: table => new
                {
                    Scientist_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scientist", x => x.Scientist_Id);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    Mission_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Launch_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedDuration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mission_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rocket_Id = table.Column<int>(type: "int", nullable: false),
                    Launchpad_Id = table.Column<int>(type: "int", nullable: false),
                    Manager_Id = table.Column<int>(type: "int", nullable: false),
                    CelestialBody_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.Mission_Id);
                    table.ForeignKey(
                        name: "FK_Mission_CelestialBody_CelestialBody_Id",
                        column: x => x.CelestialBody_Id,
                        principalTable: "CelestialBody",
                        principalColumn: "Body_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mission_Launchpad_Launchpad_Id",
                        column: x => x.Launchpad_Id,
                        principalTable: "Launchpad",
                        principalColumn: "LaunchPad_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mission_Manager_Manager_Id",
                        column: x => x.Manager_Id,
                        principalTable: "Manager",
                        principalColumn: "Manager_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mission_Rocket_Rocket_Id",
                        column: x => x.Rocket_Id,
                        principalTable: "Rocket",
                        principalColumn: "Rocket_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Experiment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mission_id = table.Column<int>(type: "int", nullable: false),
                    ScientistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Experiment_id);
                    table.ForeignKey(
                        name: "FK_Experiments_Mission_Mission_id",
                        column: x => x.Mission_id,
                        principalTable: "Mission",
                        principalColumn: "Mission_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiments_Scientist_ScientistId",
                        column: x => x.ScientistId,
                        principalTable: "Scientist",
                        principalColumn: "Scientist_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionAstronauts",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    AstronautId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionAstronauts", x => new { x.MissionId, x.AstronautId });
                    table.ForeignKey(
                        name: "FK_MissionAstronauts_Astronaut_AstronautId",
                        column: x => x.AstronautId,
                        principalTable: "Astronaut",
                        principalColumn: "Astronaunt_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionAstronauts_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Mission_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionScientists",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    ScientistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionScientists", x => new { x.MissionId, x.ScientistId });
                    table.ForeignKey(
                        name: "FK_MissionScientists_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Mission_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionScientists_Scientist_ScientistId",
                        column: x => x.ScientistId,
                        principalTable: "Scientist",
                        principalColumn: "Scientist_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Astronaut",
                columns: new[] { "Astronaunt_Id", "Hours_Simulation", "Hours_Space", "Name", "Paygrade", "Rank" },
                values: new object[,]
                {
                    { 1, 900, 420, "Anna Holm", "A1", "Commander" },
                    { 2, 1100, 860, "Jonas Madsen", "A2", "Pilot" },
                    { 3, 700, 210, "Sara Poulsen", "A2", "Mission Specialist" }
                });

            migrationBuilder.InsertData(
                table: "CelestialBody",
                columns: new[] { "Body_Id", "Body_Type", "Distance", "Name" },
                values: new object[,]
                {
                    { 1, "Moon", 384400.0, "Moon" },
                    { 2, "Planet", 225000000.0, "Mars" },
                    { 3, "Moon", 628300000.0, "Europa" }
                });

            migrationBuilder.InsertData(
                table: "Launchpad",
                columns: new[] { "LaunchPad_Id", "Location", "Max_Weight_Supported", "Status" },
                values: new object[,]
                {
                    { 1, "Aarhus North", 20000.0, "Available" },
                    { 2, "Aarhus South", 25000.0, "Available" }
                });

            migrationBuilder.InsertData(
                table: "Manager",
                columns: new[] { "Manager_Id", "Department", "Hire_Date", "Name" },
                values: new object[,]
                {
                    { 1, "Mission Operations", new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maja Jensen" },
                    { 2, "Flight Planning", new DateTime(2020, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lucas Nielsen" }
                });

            migrationBuilder.InsertData(
                table: "Rocket",
                columns: new[] { "Rocket_Id", "Crew_Capacity", "Fuel_Capacity", "Model", "Payload_capacity", "Stages", "Weight" },
                values: new object[,]
                {
                    { 1, 4, 120000.0, "Falcon A", 5000.0, 2, 12000.0 },
                    { 2, 6, 180000.0, "Orion X", 7500.0, 3, 18000.0 }
                });

            migrationBuilder.InsertData(
                table: "Scientist",
                columns: new[] { "Scientist_Id", "HireDate", "Name", "Specialty", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eva Larsen", "Astrobiology", "Dr." },
                    { 2, new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Noah Berg", "Planetary Geology", "Dr." }
                });

            migrationBuilder.InsertData(
                table: "Mission",
                columns: new[] { "Mission_Id", "CelestialBody_Id", "Launch_Date", "Launchpad_Id", "Manager_Id", "Mission_Type", "Name", "PlannedDuration", "Rocket_Id", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Research", "Luna Survey One", 14, 1, "Planned" },
                    { 2, 2, new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Exploration", "Mars Ice Mapper", 180, 2, "Created" }
                });

            migrationBuilder.InsertData(
                table: "Experiments",
                columns: new[] { "Experiment_id", "Mission_id", "ScientistId", "creation_date", "description", "name" },
                values: new object[] { 1, 1, 1, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a test", "TestExperiment" });

            migrationBuilder.InsertData(
                table: "MissionAstronauts",
                columns: new[] { "AstronautId", "MissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "MissionScientists",
                columns: new[] { "MissionId", "ScientistId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_Mission_id",
                table: "Experiments",
                column: "Mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_ScientistId",
                table: "Experiments",
                column: "ScientistId");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_CelestialBody_Id",
                table: "Mission",
                column: "CelestialBody_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_Launchpad_Id",
                table: "Mission",
                column: "Launchpad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_Manager_Id",
                table: "Mission",
                column: "Manager_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_Rocket_Id",
                table: "Mission",
                column: "Rocket_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionAstronauts_AstronautId",
                table: "MissionAstronauts",
                column: "AstronautId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionScientists_ScientistId",
                table: "MissionScientists",
                column: "ScientistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "MissionAstronauts");

            migrationBuilder.DropTable(
                name: "MissionScientists");

            migrationBuilder.DropTable(
                name: "Astronaut");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "Scientist");

            migrationBuilder.DropTable(
                name: "CelestialBody");

            migrationBuilder.DropTable(
                name: "Launchpad");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Rocket");
        }
    }
}
