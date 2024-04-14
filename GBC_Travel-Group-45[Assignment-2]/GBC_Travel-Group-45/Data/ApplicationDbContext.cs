using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_45.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GBC_Travel_Group_45.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }


         

        // New DbSets for Hotels, HotelImages, HotelBookings, Guests
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<Guest> Guests { get; set; }



        public DbSet<Car> Car { get; set; }
        public DbSet<CarBooking> CarBooking { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CarImage> CarImage { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }

}
