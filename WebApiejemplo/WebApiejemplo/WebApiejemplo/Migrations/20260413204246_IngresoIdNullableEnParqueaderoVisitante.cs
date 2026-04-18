using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class IngresoIdNullableEnParqueaderoVisitante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParqueaderoVisitante_Ingreso_IngresoId",
                table: "ParqueaderoVisitante");

            migrationBuilder.AlterColumn<int>(
                name: "IngresoId",
                table: "ParqueaderoVisitante",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ParqueaderoVisitante_Ingreso_IngresoId",
                table: "ParqueaderoVisitante",
                column: "IngresoId",
                principalTable: "Ingreso",
                principalColumn: "IngresoId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParqueaderoVisitante_Ingreso_IngresoId",
                table: "ParqueaderoVisitante");

            migrationBuilder.AlterColumn<int>(
                name: "IngresoId",
                table: "ParqueaderoVisitante",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParqueaderoVisitante_Ingreso_IngresoId",
                table: "ParqueaderoVisitante",
                column: "IngresoId",
                principalTable: "Ingreso",
                principalColumn: "IngresoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
