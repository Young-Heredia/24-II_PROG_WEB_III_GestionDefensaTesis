using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWIII_Gestion_Defensa_Tesis.Migrations
{
    /// <inheritdoc />
    public partial class fourmigration : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TypeThesis");
        }
    }
}
