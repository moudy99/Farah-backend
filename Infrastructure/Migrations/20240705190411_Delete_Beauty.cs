using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Beauty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ImagesBeautyCenter_BeautyCenter_BeautyCenterId",
                table: "ImagesBeautyCenter",
                column: "BeautyCenterId",
                principalTable: "BeautyCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceForBeautyCenter_BeautyCenter_BeautyCenterId",
                table: "ServiceForBeautyCenter",
                column: "BeautyCenterId",
                principalTable: "BeautyCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteService_Service_ServiceId",
                table: "FavoriteService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_ImagesBeautyCenter_BeautyCenter_BeautyCenterId",
    table: "ImagesBeautyCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceForBeautyCenter_BeautyCenter_BeautyCenterId",
                table: "ServiceForBeautyCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteService_Service_ServiceId",
                table: "FavoriteService");
        }
    }
}
