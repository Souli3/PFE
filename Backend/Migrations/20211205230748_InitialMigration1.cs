using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Backend.Migrations
{
    public partial class InitialMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Personnes",
                table: "Personnes");

            migrationBuilder.EnsureSchema(
                name: "projet");

            migrationBuilder.RenameTable(
                name: "Personnes",
                newName: "personnes",
                newSchema: "projet");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "projet",
                table: "personnes",
                newName: "id_personne");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "projet",
                table: "personnes",
                newName: "nom");

            migrationBuilder.AlterColumn<int>(
                name: "id_personne",
                schema: "projet",
                table: "personnes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_personnes",
                schema: "projet",
                table: "personnes",
                column: "id_personne");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_personnes",
                schema: "projet",
                table: "personnes");

            migrationBuilder.RenameTable(
                name: "personnes",
                schema: "projet",
                newName: "Personnes");

            migrationBuilder.RenameColumn(
                name: "id_personne",
                table: "Personnes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nom",
                table: "Personnes",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Personnes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personnes",
                table: "Personnes",
                column: "Id");
        }
    }
}
