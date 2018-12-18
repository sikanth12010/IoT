using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartParkingSystem.Models
{
    public class CustomerSlot
    {
        public string car_park_id { get; set; }
        public int SlotNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Vehicle_No { get; set; }
        public int Level { get; set; }
        //public string Type { get; set; }
        //public string Loc { get; set; } 
        //public string CustomerName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string SlotStatus { get; set; }
    }
}