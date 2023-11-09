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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco_1aHora = table.Column<double>(type: "float", nullable: false),
                    Preco_HorasExtra = table.Column<double>(type: "float", nullable: false),
                    Preco_Mensalista = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garagem", x => x.Id);
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
