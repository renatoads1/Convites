using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Convite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CadastroCompleto",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CriaEvento",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadastroCompleto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CriaEvento",
                table: "AspNetUsers");
        }
    }
}
