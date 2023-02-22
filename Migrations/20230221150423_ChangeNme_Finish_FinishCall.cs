using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    public partial class ChangeNme_Finish_FinishCall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Finish",
                table: "Profiles",
                newName: "FinishCall");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FinishCall",
                table: "Profiles",
                newName: "Finish");
        }
    }
}
