using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Customer : Entity
    {
        public Customer(Customer customer)
        {
            UserName = customer.UserName;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            Phone = customer.Phone;
            Photo = customer.Photo;
            Password = customer.Password;
        }

        public Customer()
        {

        }
        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }
       
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public int Phone { get; set; }

        [BsonElement("photo")]
        public string Photo { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }

    }

    public class CustomerDetails : Customer
    {
        public List<Slot> BookingDetails;
        
        public CustomerDetails(Customer customerDetails,List<Slot> bookingDetails)
            :base(customerDetails)
        {
            BookingDetails = bookingDetails;
        }

        public CustomerDetails()
            :base()
        {

        }
    }
}
