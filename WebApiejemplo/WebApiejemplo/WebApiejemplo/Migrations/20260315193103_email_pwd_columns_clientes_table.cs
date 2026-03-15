using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class email_pwd_columns_clientes_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CLIENTE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "CLIENTE",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "CLIENTE");
        }
    }
}
