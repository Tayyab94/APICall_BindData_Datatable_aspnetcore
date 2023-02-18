using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    public partial class firstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aggression = table.Column<double>(type: "float", nullable: false),
                    Arousal = table.Column<double>(type: "float", nullable: false),
                    Atmosphere = table.Column<double>(type: "float", nullable: false),
                    ClStress = table.Column<double>(type: "float", nullable: false),
                    Concentration = table.Column<double>(type: "float", nullable: false),
                    Discomfort = table.Column<double>(type: "float", nullable: false),
                    Excitement = table.Column<double>(type: "float", nullable: false),
                    Hesitation = table.Column<double>(type: "float", nullable: false),
                    Imagination = table.Column<double>(type: "float", nullable: false),
                    Joy = table.Column<double>(type: "float", nullable: false),
                    MentalEffort = table.Column<double>(type: "float", nullable: false),
                    Sad = table.Column<double>(type: "float", nullable: false),
                    Stress = table.Column<double>(type: "float", nullable: false),
                    Uncertainty = table.Column<double>(type: "float", nullable: false),
                    Uneasy = table.Column<double>(type: "float", nullable: false),
                    DiscomfortEnd = table.Column<double>(type: "float", nullable: false),
                    DiscomfortStart = table.Column<double>(type: "float", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewTagsString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SegmentsJSONObj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadersJSONObj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadersPositionsJSONObj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeWholeJSONObj = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Segments");
        }
    }
}
