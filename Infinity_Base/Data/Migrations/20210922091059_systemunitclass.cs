using Microsoft.EntityFrameworkCore.Migrations;

namespace Infinity_Base.Data.Migrations
{
    public partial class systemunitclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemUnitClassLibLvl1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUnitClassLibLvl1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUnitClassLibLvl2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lvl1Id = table.Column<int>(type: "int", nullable: false),
                    SystemUnitClassLibLvl1Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUnitClassLibLvl2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUnitClassLibLvl2_SystemUnitClassLibLvl1_SystemUnitClassLibLvl1Id",
                        column: x => x.SystemUnitClassLibLvl1Id,
                        principalTable: "SystemUnitClassLibLvl1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemUnitClassLibLvl2_SystemUnitClassLibLvl1Id",
                table: "SystemUnitClassLibLvl2",
                column: "SystemUnitClassLibLvl1Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemUnitClassLibLvl2");

            migrationBuilder.DropTable(
                name: "SystemUnitClassLibLvl1");
        }
    }
}
