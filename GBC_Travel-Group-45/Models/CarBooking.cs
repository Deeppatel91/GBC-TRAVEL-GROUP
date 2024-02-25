namespace GBC_Travel_Group_45.Models
{
    public class CarBooking
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public string PickupLocation { get; set; }
        public string DropLocation { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public decimal TotalPrice { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
    }
}