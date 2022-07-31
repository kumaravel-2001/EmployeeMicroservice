using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Infrastructure.Data.Migrations
{
    public partial class InitialDBandtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(nullable: false),
                    ManagerID = table.Column<int>(nullable: false),
                    Pending_task = table.Column<string>(nullable: false),
                    Due_Date = table.Column<DateTime>(nullable: false),
                    Work_assigned_by = table.Column<string>(nullable: false),
                    status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoLists");
        }
    }
}
