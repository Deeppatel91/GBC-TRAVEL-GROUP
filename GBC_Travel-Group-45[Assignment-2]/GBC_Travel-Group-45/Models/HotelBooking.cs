using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_45.Models
{
    public class HotelBooking

    {
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public int GuestId { get; set; }

        [Required]
        public DateTime BookingStart { get; set; }

        [Required]
        public DateTime BookingEnd { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [ForeignKey("GuestId")]
        public virtual Guest Guest { get; set; }

        public string RoomType { get; set; } 
        public int NumberOfRooms { get; set; }
        public decimal TotalPrice { get; set; } 

    }
}
