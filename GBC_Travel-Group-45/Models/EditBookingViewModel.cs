using Microsoft.AspNetCore.Mvc.Rendering;

namespace GBC_Travel_Group_45.Models
{
    public class EditBookingViewModel
    {
        public int BookingId { get; set; }
        public int SelectedFlightId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> AvailableFlights { get; set; } = new List<SelectListItem>();
    }

}
