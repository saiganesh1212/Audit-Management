using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditClient.Migrations
{
    public partial class initialmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    DateofExecution = table.Column<DateTime>(nullable: false),
                    AuditId = table.Column<int>(nullable: false),
                    ProjectExecutionStatus = table.Column<string>(nullable: false),
                    RemedialActionDuration = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditResponse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditResponse");
        }
    }
}
