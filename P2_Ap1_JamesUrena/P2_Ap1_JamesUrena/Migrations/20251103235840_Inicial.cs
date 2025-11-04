using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P2_Ap1_JamesUrena.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    ComponenteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false),
                    Existencia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.ComponenteId);
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NombreCliente = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidoDetalles",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComponenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoDetalles", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_PedidoDetalles_Componente_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componente",
                        principalColumn: "ComponenteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoDetalles_Registros_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Registros",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Componente",
                columns: new[] { "ComponenteId", "Descripcion", "Existencia", "Precio" },
                values: new object[,]
                {
                    { 1, "Memoria 4GB", 1, 1580.0 },
                    { 2, "Disco SSD 120MB", 8, 4200.0 },
                    { 3, "Tarjeta de Video", 4, 10000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_ComponenteId",
                table: "PedidoDetalles",
                column: "ComponenteId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_PedidoId",
                table: "PedidoDetalles",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoDetalles");

            migrationBuilder.DropTable(
                name: "Componente");

            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
