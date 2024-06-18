using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_servicesForBeautyCenter_BeautyCenters_BeautyCenterId",
    table: "servicesForBeautyCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_servicesForBeautyCenter_BeautyCenters_BeautyCenterId",
                table: "servicesForBeautyCenter",
                column: "BeautyCenterId",
                principalTable: "BeautyCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
