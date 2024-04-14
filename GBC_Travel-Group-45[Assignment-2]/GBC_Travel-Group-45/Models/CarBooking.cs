using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_45.Models
{
    public class CarBooking
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        [Required]
        public DateTime BookingStart { get; set; }

        [Required]
        public DateTime BookingEnd { get; set; }

        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropLocation { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; } 
        public decimal TotalPrice { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
    }
}