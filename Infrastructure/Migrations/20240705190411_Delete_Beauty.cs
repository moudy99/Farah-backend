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
            // ImagesBeautyCenter -> BeautyCenters
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

            // servicesForBeautyCenter -> BeautyCenters
            migrationBuilder.DropForeignKey(
                name: "FK_servicesForBeautyCenter_BeautyCenters_BeautyCenterId",
                table: "servicesForBeautyCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_servicesForBeautyCenter_BeautyCenters_BeautyCenterId",
                table: "servicesForBeautyCenter",
                column: "BeautyCenterId",
                principalTable: "BeautyCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // FavoriteService -> Services
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteService_Services_ServiceId",
                table: "FavoriteService");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteService_Services_ServiceId",
                table: "FavoriteService",
                column: "ServiceId",
                principalTable: "Services",
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
                principalColumn: "Id");

            // servicesForBeautyCenter -> BeautyCenters
            migrationBuilder.DropForeignKey(
                name: "FK_servicesForBeautyCenter_BeautyCenters_BeautyCenterId",
                table: "servicesForBeautyCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_servicesForBeautyCenter_BeautyCenters_BeautyCenterId",
                table: "servicesForBeautyCenter",
                column: "BeautyCenterId",
                principalTable: "BeautyCenters",
                principalColumn: "Id");

            // FavoriteService -> Services
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteService_Services_ServiceId",
                table: "FavoriteService");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteService_Services_ServiceId",
                table: "FavoriteService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
