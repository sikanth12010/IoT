using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ParkingService.Controllers;
using ParkingService.Models;

namespace ParkingService.Test
{
    [TestClass]
    public class Usertest
    {
        UserController obj = new UserController();

        [TestMethod]
        public void SaveUser()
        {
            User objUser = new User();
            objUser.Email = "d1.sahu@gmail.com";
            objUser.FirstName = "Deepak1";
            objUser.LastName = "Sahu1";
            objUser.Username = "deep";
            objUser.MobileNo = "9898989898";
            objUser.VehicleRegNo = "MH 08 4743";
            objUser.PasswordHash = Encoding.ASCII.GetBytes("someString");
            objUser.PasswordSalt = Encoding.ASCII.GetBytes("someString");
            obj.AddUser(objUser, "Deepak#123");


        }


    }
}
