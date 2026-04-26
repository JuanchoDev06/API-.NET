using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaCuartosUtil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuartosUtil",
                columns: table => new
                {
                    CuartoUtilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UnidadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuartosUtil", x => x.CuartoUtilId);
                    table.ForeignKey(
                        name: "FK_CuartosUtil_Apartamentos_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Apartamentos",
                        principalColumn: "UnidadId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuartosUtil_UnidadId",
                table: "CuartosUtil",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "UQ_CuartoUtil_Numero",
                table: "CuartosUtil",
                column: "Numero",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuartosUtil");
        }
    }
}
