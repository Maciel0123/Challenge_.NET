using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PATIOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZONAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PatioId = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZONAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZONA_PATIO",
                        column: x => x.PatioId,
                        principalTable: "PATIOS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MOTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    MODELO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PLACA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ZonaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MOTO_ZONA",
                        column: x => x.ZonaId,
                        principalTable: "ZONAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MOTOS_ZonaId",
                table: "MOTOS",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZONAS_PatioId",
                table: "ZONAS",
                column: "PatioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOTOS");

            migrationBuilder.DropTable(
                name: "ZONAS");

            migrationBuilder.DropTable(
                name: "PATIOS");
        }
    }
}
