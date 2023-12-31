using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_API_minim.Migrations
{
    /// <inheritdoc />
    public partial class AddClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Heroes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Heroes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_ClassId",
                table: "Heroes",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Classes_ClassId",
                table: "Heroes",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Classes_ClassId",
                table: "Heroes");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_ClassId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Heroes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Heroes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
