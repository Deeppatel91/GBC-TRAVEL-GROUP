using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_45.Migrations
{
    /// <inheritdoc />
    public partial class CarRentals1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatingCapacity",
                table: "Cars",
                newName: "NumOfSeats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfSeats",
                table: "Cars",
                newName: "SeatingCapacity");
        }
    }
}
