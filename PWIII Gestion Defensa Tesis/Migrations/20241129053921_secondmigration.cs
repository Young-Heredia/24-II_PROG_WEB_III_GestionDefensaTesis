using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWIII_Gestion_Defensa_Tesis.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "TypeThesis",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Thesis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TypeThesis");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Thesis");
        }
    }
}
