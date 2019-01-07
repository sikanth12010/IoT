using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartParkingSystem.Models
{
    public class Owner
    {
        public string _Id { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }
        public string OwnerType { get; set; }
        public string OwnedParkingSpace { get; set; }
    }
}