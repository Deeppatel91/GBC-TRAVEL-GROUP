using Microsoft.AspNetCore.Identity;

namespace GBC_Travel_Group_45.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

      

    }
}
