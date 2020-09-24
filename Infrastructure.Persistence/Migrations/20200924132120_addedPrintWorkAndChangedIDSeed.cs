using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class addedPrintWorkAndChangedIDSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrintWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterId = table.Column<int>(type: "int", nullable: true),
                    PaidWhen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrintStartWhen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkFinishWhen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintWorks_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrintWorks_PrinterId",
                table: "PrintWorks",
                column: "PrinterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrintWorks");
        }
    }
}
