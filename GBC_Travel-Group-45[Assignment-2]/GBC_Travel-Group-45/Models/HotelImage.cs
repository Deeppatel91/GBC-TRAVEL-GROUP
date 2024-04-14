using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_45.Models
{
    public class HotelImage
    {
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}
