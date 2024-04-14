using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_45.Migrations
{
    /// <inheritdoc />
    public partial class BookCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Car_CarId",
                table: "CarImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarImages",
                table: "CarImages");

            migrationBuilder.RenameTable(
                name: "CarImages",
                newName: "CarImage");

            migrationBuilder.RenameIndex(
                name: "IX_CarImages_CarId",
                table: "CarImage",
                newName: "IX_CarImage_CarId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CarBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CarBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarImage",
                table: "CarImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImage_Car_CarId",
                table: "CarImage",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImage_Car_CarId",
                table: "CarImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarImage",
                table: "CarImage");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CarBookings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CarBookings");

            migrationBuilder.RenameTable(
                name: "CarImage",
                newName: "CarImages");

            migrationBuilder.RenameIndex(
                name: "IX_CarImage_CarId",
                table: "CarImages",
                newName: "IX_CarImages_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarImages",
                table: "CarImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Car_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
