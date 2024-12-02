using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWIII_Gestion_Defensa_Tesis.Migrations
{
    /// <inheritdoc />
    public partial class dataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audience",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audience", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Professional",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    lastName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    secondLastName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    career = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professional", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    lastName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    secondLastName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    ci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeThesis",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeThesis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Rols_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Rols",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thesis",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    description = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Note = table.Column<double>(type: "float", nullable: false),
                    idTypeThesis = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thesis", x => x.id);
                    table.ForeignKey(
                        name: "FK_Thesis_TypeThesis",
                        column: x => x.idTypeThesis,
                        principalTable: "TypeThesis",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DefenseActivity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    defenseDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    idThesis = table.Column<int>(type: "int", nullable: false),
                    idAudience = table.Column<int>(type: "int", nullable: false),
                    idStudent = table.Column<short>(type: "smallint", nullable: false),
                    StatusThesis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.id);
                    table.ForeignKey(
                        name: "FK_Activity_Audience",
                        column: x => x.idAudience,
                        principalTable: "Audience",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Activity_Student",
                        column: x => x.idStudent,
                        principalTable: "Student",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Activity_Thesis",
                        column: x => x.idThesis,
                        principalTable: "Thesis",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityProfessional",
                columns: table => new
                {
                    idActivityProfessional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActivity = table.Column<int>(type: "int", nullable: false),
                    idProfessional = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityProfessional", x => x.idActivityProfessional);
                    table.ForeignKey(
                        name: "FK_ActivityProfessional_Activity",
                        column: x => x.idActivity,
                        principalTable: "DefenseActivity",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ActivityProfessional_Professional",
                        column: x => x.idProfessional,
                        principalTable: "Professional",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProfessional_idActivity",
                table: "ActivityProfessional",
                column: "idActivity");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProfessional_idProfessional",
                table: "ActivityProfessional",
                column: "idProfessional");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseActivity_idAudience",
                table: "DefenseActivity",
                column: "idAudience");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseActivity_idStudent",
                table: "DefenseActivity",
                column: "idStudent");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseActivity_idThesis",
                table: "DefenseActivity",
                column: "idThesis");

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_idTypeThesis",
                table: "Thesis",
                column: "idTypeThesis");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityProfessional");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DefenseActivity");

            migrationBuilder.DropTable(
                name: "Professional");

            migrationBuilder.DropTable(
                name: "Rols");

            migrationBuilder.DropTable(
                name: "Audience");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Thesis");

            migrationBuilder.DropTable(
                name: "TypeThesis");
        }
    }
}
