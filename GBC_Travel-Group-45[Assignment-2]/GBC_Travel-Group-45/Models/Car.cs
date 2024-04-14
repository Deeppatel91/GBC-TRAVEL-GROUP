namespace GBC_Travel_Group_45.Models { 
public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
        public string Color { get; set; }
        public int NumOfSeats { get; set; }
        public decimal PricePerDay { get; set; }
        public string CarType { get; set; }
       
    

        public virtual ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();

        public virtual ICollection<CarBooking> CarBooking { get; set; } = new List<CarBooking>();
    }
    }