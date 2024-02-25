using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_45.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        public string HotelName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int SingleRoomsCount { get; set; }

        [Required]
        public int DoubleRoomsCount { get; set; }

        [Required]
        public decimal SingleRoomPrice { get; set; }

        [Required]
        public decimal DoubleRoomPrice { get; set; }

        public virtual ICollection<HotelImage> Images { get; set; } = new List<HotelImage>();

        public virtual ICollection<HotelBooking> Bookings { get; set; } = new List<HotelBooking>();
    }
}
