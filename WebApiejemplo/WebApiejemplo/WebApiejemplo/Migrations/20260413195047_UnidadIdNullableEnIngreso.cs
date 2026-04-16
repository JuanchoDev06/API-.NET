using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class UnidadIdNullableEnIngreso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingreso_Apartamentos_UnidadId",
                table: "Ingreso");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadId",
                table: "Ingreso",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingreso_Apartamentos_UnidadId",
                table: "Ingreso",
                column: "UnidadId",
                principalTable: "Apartamentos",
                principalColumn: "UnidadId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingreso_Apartamentos_UnidadId",
                table: "Ingreso");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadId",
                table: "Ingreso",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingreso_Apartamentos_UnidadId",
                table: "Ingreso",
                column: "UnidadId",
                principalTable: "Apartamentos",
                principalColumn: "UnidadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
