using GBC_Travel_Group_45.Models;
using System;
using System.Collections.Generic;

namespace GBC_Travel_Group_45.ViewModels
{
    public class HotelBookingViewModel
    {
        public int HotelId { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public int GuestAge { get; set; }
        public string GovernmentId { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }

        
        public Hotel Hotel { get; set; } 
    }
}
