using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class cuartamigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EVMNET");

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                schema: "EVMNET",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESA",
                schema: "EVMNET",
                columns: table => new
                {
                    IDEMPRESA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBREEMPRESA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.IDEMPRESA);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE",
                schema: "EVMNET");

            migrationBuilder.DropTable(
                name: "EMPRESA",
                schema: "EVMNET");
        }
    }
}
