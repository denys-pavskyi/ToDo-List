using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskCategoryId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskCategories_TaskCategoryId",
                        column: x => x.TaskCategoryId,
                        principalTable: "TaskCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TaskCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Work" },
                    { 2, "Finances" },
                    { 3, "Education" },
                    { 4, "Home" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Name", "Status", "TaskCategoryId" },
                values: new object[,]
                {
                    { 1, "Complete project proposal", 1, 1 },
                    { 2, "Schedule meetings for the team", 0, 1 },
                    { 3, "Prepare presentation for the client meeting", 0, 1 },
                    { 4, "Budget planning for the month", 0, 2 },
                    { 5, "Pay bills", 1, 2 },
                    { 6, "Review and update investments", 0, 2 },
                    { 7, "Study for upcoming exam", 0, 3 },
                    { 8, "Complete an online course", 1, 3 },
                    { 9, "Research for a project", 0, 3 },
                    { 10, "Clean and organize the living room", 3, 4 },
                    { 11, "Grocery shopping", 0, 4 },
                    { 12, "Plan home maintenance tasks", 0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskCategoryId",
                table: "Tasks",
                column: "TaskCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskCategories");
        }
    }
}
