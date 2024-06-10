using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixFav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteService_Customers_CustomerId1",
                table: "FavoriteService");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteService_CustomerId1",
                table: "FavoriteService");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "FavoriteService");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "FavoriteService",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteService_CustomerId",
                table: "FavoriteService",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteService_Customers_CustomerId",
                table: "FavoriteService",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteService_Customers_CustomerId",
                table: "FavoriteService");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteService_CustomerId",
                table: "FavoriteService");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "FavoriteService",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "FavoriteService",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteService_CustomerId1",
                table: "FavoriteService",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteService_Customers_CustomerId1",
                table: "FavoriteService",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
