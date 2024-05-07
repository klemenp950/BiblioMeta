using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioMeta.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avtor",
                columns: table => new
                {
                    AvtorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priimek = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avtor", x => x.AvtorID);
                });

            migrationBuilder.CreateTable(
                name: "Knjiga",
                columns: table => new
                {
                    KnjigaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StStrani = table.Column<int>(type: "int", nullable: false),
                    StZnakov = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<float>(type: "real", nullable: false),
                    ZanrID = table.Column<int>(type: "int", nullable: false),
                    AvtorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjiga", x => x.KnjigaID);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    ZanrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeZanra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.ZanrID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avtor");

            migrationBuilder.DropTable(
                name: "Knjiga");

            migrationBuilder.DropTable(
                name: "Zanr");
        }
    }
}
