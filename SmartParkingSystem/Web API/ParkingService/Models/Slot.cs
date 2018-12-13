using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Slot:Entity
    {
        [BsonElement("car_park_id")]
        public string car_park_id { get; set; }
        [BsonElement("cust_id")]
        public string cust_id { get; set; }
        [BsonElement("vehicle_no")]
        public string vehicle_no { get; set; }
        [BsonElement("type")]
        public string type { get; set; }
        [BsonElement("loc")]
        public string loc { get; set; }
        [BsonElement("level")]
        public int Level { get; set; }
        [BsonElement("slot_status")]
        public string SlotStatus { get; set; }
        [BsonElement("slot_no")]
        public int SlotNumber { get; set; }


    }
}
