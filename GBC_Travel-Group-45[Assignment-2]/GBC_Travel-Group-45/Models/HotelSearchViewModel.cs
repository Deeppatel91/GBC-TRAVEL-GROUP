
namespace GBC_Travel_Group_45.Models
{
    public class HotelSearchViewModel
    {
        public string Location { get; set; }
        public DateTime? BookingStart { get; set; }
        public DateTime? BookingEnd { get; set; }
        public int? Guests { get; set; }
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
