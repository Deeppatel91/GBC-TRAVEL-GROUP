namespace GBC_Travel_Group_45.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }

        public string Name { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }

}
