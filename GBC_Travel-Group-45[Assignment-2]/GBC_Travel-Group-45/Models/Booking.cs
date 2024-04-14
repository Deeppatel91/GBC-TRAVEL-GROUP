namespace GBC_Travel_Group_45.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public DateTime BookingDate { get; set; }

        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
    }

}
