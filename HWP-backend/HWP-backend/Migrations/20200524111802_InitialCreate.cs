using Microsoft.EntityFrameworkCore.Migrations;

namespace HWP_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Tasks",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Tasks", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "SolvedTasks",
                table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    IsChecked = table.Column<bool>(nullable: false),
                    Mark = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolvedTasks", x => new {x.UserId, x.TaskId});
                    table.ForeignKey(
                        "FK_SolvedTasks_Tasks_TaskId",
                        x => x.TaskId,
                        "Tasks",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_SolvedTasks_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_SolvedTasks_TaskId",
                "SolvedTasks",
                "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SolvedTasks");

            migrationBuilder.DropTable(
                "Tasks");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}