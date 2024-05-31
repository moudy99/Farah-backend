using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassDressesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Dresses");

            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Dresses");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Dresses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopName",
                table: "Dresses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ShopDescription",
                table: "Dresses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "GovernorateID",
                table: "Dresses",
                newName: "ShopId");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDFrontImage",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDBackImage",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountStatus",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Dresses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.CreateTable(
                name: "ShopDresses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GovernorateID = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    OpeningHours = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopDresses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dresses_ShopId",
                table: "Dresses",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dresses_ShopDresses_ShopId",
                table: "Dresses",
                column: "ShopId",
                principalTable: "ShopDresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dresses_ShopDresses_ShopId",
                table: "Dresses");

            migrationBuilder.DropTable(
                name: "ShopDresses");

            migrationBuilder.DropIndex(
                name: "IX_Dresses_ShopId",
                table: "Dresses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Dresses");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dresses",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Dresses",
                newName: "GovernorateID");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dresses",
                newName: "ShopName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Dresses",
                newName: "ShopDescription");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Owners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IDFrontImage",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IDBackImage",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountStatus",
                table: "Owners",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "City",
                table: "Dresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningHours",
                table: "Dresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21,
                oldNullable: true);
        }
    }
}
