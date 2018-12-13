using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace ParkingService.Controllers
{
    [Produces("application/json")]
    // [Route("api/SlotBook")]
    public class SlotBookController : Controller
    {
        // GET: api/SlotBook
        [HttpGet]
        [Route("api/SlotBook/GetUserList/{id}")]
        public string GetUserList(string id)
        {
            var SlotList = MongoDBHelper.GetEntityList<Slot>();
            var ParkingInformation = MongoDBHelper.SearchByObjectID<CarPark>(id);
            var customerList = MongoDBHelper.GetEntityList<Customer>();
            var res = from s in SlotList
                      join c in customerList on s.cust_id equals c._id
                      select new {
                          ParkingInformation.name,
                          ParkingInformation.aspaces,
                          ParkingInformation.tspaces,
                          s.vehicle_no, c.FirstName,c.LastName, c.Phone, c.Email };

            var Json = JsonConvert.SerializeObject(res);
            return Json;
        }

        // GET: api/SlotBook/5
        // GET: api/Park/5
        //[HttpGet("{id}")]
        [Route("api/SlotBook/Booked")]          
        public string Get(string carpark_id,int slot_id)
        {
            AuthResponse auth = new AuthResponse();
            CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(carpark_id);

            //List<Slot> slotDetails = MongoDBHelper.GetEntityList<Slot>();
            Slot slotDetail = MongoDBHelper.SearchByQueryObject<Slot>(Query.EQ("slot_no", slot_id), "Slot");   
            //Slot slotDetail = slotDetails.FirstOrDefault(s => s.SlotNumber == slot_id);
            if (location != null && slotDetail != null)
            {
                
                if(slotDetail.SlotStatus == "Booked")             //If  booked through Mobile App
                {
                    slotDetail.SlotStatus = "Parked";
                    MongoDBHelper.InsertEntity<Slot>(slotDetail);
                }
                else if(slotDetail.SlotStatus == "Empty")          //If Not booked through Mobile App
                {
                    location.aspaces = location.aspaces - 1;
                    MongoDBHelper.InsertEntity<CarPark>(location);

                    slotDetail.SlotStatus = "Parked";
                    MongoDBHelper.InsertEntity<Slot>(slotDetail);
                }
                auth.Status = 0;
                return JsonConvert.SerializeObject(auth);
            }
            auth.Status = 1;
            return JsonConvert.SerializeObject(auth);
        }



        // POST: api/SlotBook
        //        {
        //    "car_park_id" : "5af30d6acdafc0b5115d5752",
        //    "cust_id" : "5b4732b29e778f23c843b9f4",
        //    "vehicle_no" : "MH-04-DR-1234",
        //    "bookingtime":"10-03-18 12:00"
        //}


        [HttpPost]
        [Route("api/SlotBook/Post")]       //called from mobile App
        public void Post([FromBody]Slot value)
        {
            //Update carpark location
            CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(value.car_park_id);
            if (location != null)
            {
                location.aspaces = location.aspaces - 1;
                MongoDBHelper.InsertEntity<CarPark>(location);
            }
            value.SlotStatus = "Booked";
            MongoDBHelper.InsertEntity<Slot>(value);
            // JObject json = JObject.Parse(value);

        }

        // PUT: api/SlotBook/5
        [HttpPut("{id}")]
        [Route("api/SlotBook")]
        public string Put(string id, [FromBody]string value)
        {
            AuthResponse auth = new AuthResponse();
            CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(id);
            if (location != null)
            {
                location.aspaces = location.aspaces + 1;
                MongoDBHelper.InsertEntity<CarPark>(location);
                auth.Status = 0;
                return JsonConvert.SerializeObject(auth);
            }
            auth.Status = 1;
            return JsonConvert.SerializeObject(auth);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Route("api/SlotBook/Delete")]
        public void Delete(int id)
        {
        }
    }
}
