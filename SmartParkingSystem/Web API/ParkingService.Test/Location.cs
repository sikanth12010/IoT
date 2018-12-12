using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingService.Controllers;
using ParkingService.Models;
using System.Collections.Generic;

namespace ParkingService.Test
{
    [TestClass]
    public class Location
    {
        [TestMethod]
        public void GetParkAreas()
        {
            LocationController parkcontroller = new LocationController();
            var locations = parkcontroller.GetAll();
        }

        [TestMethod]
        public void GetParkAreasByLocation()
        {
            List<double> lst = new List<double>();
            lst.Add(1.23222);
            lst.Add(4.56222);
            var loc = new Loc() { type = "point", coordinates = lst };
            LocationController parkcontroller = new LocationController();
            var locations = parkcontroller.GetByLatLong(loc);
        }

        [TestMethod]
        public void AddLocation()
        {
            LocationController parkcontroller = new LocationController();
            parkcontroller.AddLocation();
        }
    }
}
