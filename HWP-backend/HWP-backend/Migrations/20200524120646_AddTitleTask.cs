using Microsoft.EntityFrameworkCore.Migrations;

namespace HWP_backend.Migrations
{
    public partial class AddTitleTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Title",
                "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Title",
                "Tasks");
        }
    }
}