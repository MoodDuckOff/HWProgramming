using Microsoft.EntityFrameworkCore.Migrations;

namespace HWP_backend.Migrations
{
    public partial class AddSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Solution",
                "SolvedTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Solution",
                "SolvedTasks");
        }
    }
}