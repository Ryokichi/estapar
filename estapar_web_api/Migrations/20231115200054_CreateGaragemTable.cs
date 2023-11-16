using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace estapar_web_api.Migrations
{
    /// <inheritdoc />
    public partial class CreateGaragemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garagem",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco_1aHora = table.Column<double>(type: "float", nullable: false),
                    Preco_HorasExtra = table.Column<double>(type: "float", nullable: false),
                    Preco_Mensalista = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garagem", x => x.Codigo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Garagem");
        }
    }
}
