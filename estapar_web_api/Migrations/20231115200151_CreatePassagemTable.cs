using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace estapar_web_api.Migrations
{
    /// <inheritdoc />
    public partial class CreatePassagemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GaragemCodigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarroPlaca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarroMarca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarroModelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraSaida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormaPagamentoCodigo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrecoTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passagem_FormaPagamento_FormaPagamentoCodigo",
                        column: x => x.FormaPagamentoCodigo,
                        principalTable: "FormaPagamento",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_Passagem_Garagem_GaragemCodigo",
                        column: x => x.GaragemCodigo,
                        principalTable: "Garagem",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passagem_FormaPagamentoCodigo",
                table: "Passagem",
                column: "FormaPagamentoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Passagem_GaragemCodigo",
                table: "Passagem",
                column: "GaragemCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passagem");
        }
    }
}
