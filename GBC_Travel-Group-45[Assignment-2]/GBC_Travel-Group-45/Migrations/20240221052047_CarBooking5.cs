using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_45.Migrations
{
    /// <inheritdoc />
    public partial class CarBooking5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBookings_Car_CarId",
                table: "CarBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarBookings_Customer_CustomerId",
                table: "CarBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarBookings",
                table: "CarBookings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "CarBookings",
                newName: "CarBooking");

            migrationBuilder.RenameIndex(
                name: "IX_CarBookings_CustomerId",
                table: "CarBooking",
                newName: "IX_CarBooking_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CarBookings_CarId",
                table: "CarBooking",
                newName: "IX_CarBooking_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarBooking",
                table: "CarBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBooking_Car_CarId",
                table: "CarBooking",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarBooking_Customer_CustomerId",
                table: "CarBooking",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBooking_Car_CarId",
                table: "CarBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_CarBooking_Customer_CustomerId",
                table: "CarBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarBooking",
                table: "CarBooking");

            migrationBuilder.RenameTable(
                name: "CarBooking",
                newName: "CarBookings");

            migrationBuilder.RenameIndex(
                name: "IX_CarBooking_CustomerId",
                table: "CarBookings",
                newName: "IX_CarBookings_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CarBooking_CarId",
                table: "CarBookings",
                newName: "IX_CarBookings_CarId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarBookings",
                table: "CarBookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBookings_Car_CarId",
                table: "CarBookings",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarBookings_Customer_CustomerId",
                table: "CarBookings",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
