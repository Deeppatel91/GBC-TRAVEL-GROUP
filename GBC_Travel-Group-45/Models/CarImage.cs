using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_45.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
    }
}