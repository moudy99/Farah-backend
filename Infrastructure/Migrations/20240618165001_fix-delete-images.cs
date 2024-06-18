using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixdeleteimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                    name: "FK_ImagesBeautyCenter_BeautyCenters_BeautyCenterId",
                    table: "ImagesBeautyCenter");

                            migrationBuilder.AddForeignKey(
                                name: "FK_ImagesBeautyCenter_BeautyCenters_BeautyCenterId",
                                table: "ImagesBeautyCenter",
                                column: "BeautyCenterId",
                                principalTable: "BeautyCenters",
                                principalColumn: "Id",
                                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_ImagesBeautyCenter_BeautyCenters_BeautyCenterId",
    table: "ImagesBeautyCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesBeautyCenter_BeautyCenters_BeautyCenterId",
                table: "ImagesBeautyCenter",
                column: "BeautyCenterId",
                principalTable: "BeautyCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
