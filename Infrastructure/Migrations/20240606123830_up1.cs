using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class up1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Photograph_PhotographerId",
                table: "Portfolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolio",
                newName: "ImagePhotography");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolio_PhotographerId",
                table: "ImagePhotography",
                newName: "IX_ImagePhotography_PhotographerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagePhotography",
                table: "ImagePhotography",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagePhotography_Photograph_PhotographerId",
                table: "ImagePhotography",
                column: "PhotographerId",
                principalTable: "Photograph",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagePhotography_Photograph_PhotographerId",
                table: "ImagePhotography");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagePhotography",
                table: "ImagePhotography");

            migrationBuilder.RenameTable(
                name: "ImagePhotography",
                newName: "Portfolio");

            migrationBuilder.RenameIndex(
                name: "IX_ImagePhotography_PhotographerId",
                table: "Portfolio",
                newName: "IX_Portfolio_PhotographerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Photograph_PhotographerId",
                table: "Portfolio",
                column: "PhotographerId",
                principalTable: "Photograph",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
