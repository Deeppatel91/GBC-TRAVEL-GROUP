
namespace GBC_Travel_Group_45.Models
{
    public class FlightSearchViewModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime? DepartureDate { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<Flight> Flights { get; set; } = new List<Flight>();
    }
}
