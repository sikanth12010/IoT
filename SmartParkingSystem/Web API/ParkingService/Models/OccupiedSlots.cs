using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class OccupiedSlots
    {
        public OccupiedSlots(string car_park_id,int slotNumber, string firstName, string lastName, string vehicle_no, int level, string email, int phone, string slotStatus)
        {
            this.car_park_id = car_park_id;
            SlotNumber = slotNumber;
            FirstName = firstName;
            LastName = lastName;
            this.vehicle_no = vehicle_no;
            Level = level;
            Email = email;
            Phone = phone;
            SlotStatus = slotStatus;
        }

        public OccupiedSlots()
        {

        }

        public string car_park_id { get; set; }
        public int SlotNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string vehicle_no { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string SlotStatus { get; set; }

        
    }
}
