using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_45.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string GovernmentId { get; set; }

        public virtual ICollection<HotelBooking> Bookings { get; set; } = new List<HotelBooking>();
    }
}
