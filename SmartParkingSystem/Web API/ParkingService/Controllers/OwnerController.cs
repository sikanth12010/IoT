using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using ParkingService.Common;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    //[Produces("application/json")]
    
    public class OwnerController : Controller
    {
        [HttpGet]
        [Route("api/Owner")]
        public string Authenticate(string username, string password)
        {
            Owner ownerResponse = new Owner();
            ownerResponse.Status = 2;               //Authentication failed
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var OwnerList = MongoDBHelper.GetEntityList<Owner>();
                    ownerResponse = OwnerList.Find(x => x.UserName == username && x.Password == password);
                    ownerResponse.Status = ownerResponse != null ? 0 : 2;
                }
            }
            catch (System.Exception)
            {
                ownerResponse.Status = 1;           //Exception Occured
            }
            return JsonConvert.SerializeObject(ownerResponse);
        }

        [Route("api/GetAllOwners")]
        public string GetAllOwners()
        {
            List<Owner> ownerList = MongoDBHelper.GetEntityList<Owner>();
            List<CarPark> carParkList = MongoDBHelper.GetEntityList<CarPark>();
            var result = from c in carParkList
                         join o in ownerList on c.OwnerID equals o._id
                         select new
                         {
                             c.name,
                             c.OwnerID
                         };

            List<Owner> oList = new List<Owner>();
            foreach(var row1 in result)
            {
                foreach(var row2 in ownerList)
                {
                    if(row1.OwnerID == row2._id)
                    {
                        row2.OwnedParkingSpace = row1.name;
                        oList.Add(row2);
                    }
                }
            }

            var json = JsonConvert.SerializeObject(oList);
            return json;
        }
    }
    
}