using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class CambioInFoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_foto_Category_CategoryId",
                table: "foto");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_foto_CategoryId",
                table: "foto");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "foto");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "foto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "foto");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "foto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_foto_CategoryId",
                table: "foto",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_foto_Category_CategoryId",
                table: "foto",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
