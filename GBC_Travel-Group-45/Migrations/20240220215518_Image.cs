using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_45.Migrations
{
    /// <inheritdoc />
    public partial class Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBookings_Cars_CarId",
                table: "CarBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarImage",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBookings_Car_CarId",
                table: "CarBookings",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Car_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBookings_Car_CarId",
                table: "CarBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Car_CarId",
                table: "CarImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.AddColumn<byte[]>(
                name: "CarImage",
                table: "Cars",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBookings_Cars_CarId",
                table: "CarBookings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
