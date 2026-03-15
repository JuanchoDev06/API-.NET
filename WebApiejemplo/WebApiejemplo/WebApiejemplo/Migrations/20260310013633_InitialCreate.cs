using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EMPRESA",
                schema: "EVMNET",
                newName: "EMPRESA");

            migrationBuilder.RenameTable(
                name: "CLIENTE",
                schema: "EVMNET",
                newName: "CLIENTE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EVMNET");

            migrationBuilder.RenameTable(
                name: "EMPRESA",
                newName: "EMPRESA",
                newSchema: "EVMNET");

            migrationBuilder.RenameTable(
                name: "CLIENTE",
                newName: "CLIENTE",
                newSchema: "EVMNET");
        }
    }
}
