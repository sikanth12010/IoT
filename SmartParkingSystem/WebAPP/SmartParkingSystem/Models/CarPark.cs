using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartParkingSystem.Models
{
    public class CarPark
    {
        public string _Id { get; set; }
        public string Name { get; set; }
        public int Tspaces { get; set; }
        public int Aspaces { get; set; }
        public int Ospaces { get; set; }
        public string Owner_Id { get; set; }
        //public string OwnerName { get; set; }
    }
}