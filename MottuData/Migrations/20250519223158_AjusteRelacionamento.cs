using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuData.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZonaId",
                table: "Motos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Patios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PatioId = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zonas_Patios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "Patios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motos_ZonaId",
                table: "Motos",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zonas_PatioId",
                table: "Zonas",
                column: "PatioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Zonas_ZonaId",
                table: "Motos",
                column: "ZonaId",
                principalTable: "Zonas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motos_Zonas_ZonaId",
                table: "Motos");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "Patios");

            migrationBuilder.DropIndex(
                name: "IX_Motos_ZonaId",
                table: "Motos");

            migrationBuilder.DropColumn(
                name: "ZonaId",
                table: "Motos");
        }
    }
}
