using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_45.Migrations
{
    /// <inheritdoc />
    public partial class HotelUpadate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "HotelBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "HotelBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "HotelBookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "HotelBookings");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "HotelBookings");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "HotelBookings");
        }
    }
}
