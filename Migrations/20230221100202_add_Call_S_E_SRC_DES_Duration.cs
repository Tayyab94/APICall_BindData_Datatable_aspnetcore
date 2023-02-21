using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    public partial class add_Call_S_E_SRC_DES_Duration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallDestination",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CallDuration",
                table: "Profiles",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallSource",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Finish",
                table: "Profiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartCall",
                table: "Profiles",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallDestination",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CallDuration",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CallSource",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Finish",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "StartCall",
                table: "Profiles");
        }
    }
}
