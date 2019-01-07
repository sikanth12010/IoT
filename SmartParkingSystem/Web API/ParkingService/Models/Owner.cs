using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingService.Models
{
    //public abstract class Entity
    //{
    //    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    //    public string _id { set; get; }
    //}

    public class Owner : Entity
    {
        public int Status { get; set; }
        [BsonElement("username")]
        public string UserName { get; set; }
        [BsonElement("first_name")]
        public string FirstName { get; set; }
        [BsonElement("last_name")]
        public string LastName { get; set; }
        [BsonElement("pswd_hash")]
        public string Password { get; set; }
        [BsonElement("pswd_salt")]
        public string Pswd_salt { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("phone")]
        public int Phone { get; set; }
        [BsonElement("photo")]
        public string Photo { get; set; }
        [BsonElement("owner_type")]
        public string OwnerType { get; set; }
        public string OwnedParkingSpace { get; set; }
    }
}
