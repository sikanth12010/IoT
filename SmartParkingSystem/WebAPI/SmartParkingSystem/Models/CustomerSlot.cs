using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartParkingSystem.Models
{
    public class CustomerSlot
    {
        public string ParkingSpaceId { get; set; }
        public string SlotNo { get; set; }
        public string VehicleNo { get; set; }
        public string Type { get; set; }
        public string Loc { get; set; }
        public string SlotStatus { get; set; }
        public string Level { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}