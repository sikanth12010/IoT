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
    [Route("api/Owner")]
    public class OwnerController : Controller
    {
        [HttpGet(Name = "Auth")]
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
    }
}