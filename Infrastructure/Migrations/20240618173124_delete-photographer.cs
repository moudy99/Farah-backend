using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletephotographer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_ImagePhotography_Photograph_PhotographerId",
    table: "ImagePhotography");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagePhotography_Photograph_PhotographerId",
                table: "ImagePhotography",
                column: "PhotographerId",
                principalTable: "Photograph",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_ImagePhotography_Photograph_PhotographerId",
    table: "ImagePhotography");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagePhotography_Photograph_PhotographerId",
                table: "ImagePhotography",
                column: "PhotographerId",
                principalTable: "Photograph",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
