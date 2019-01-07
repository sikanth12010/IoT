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
    //[Produces("application/json")]
    // [Route("api/SlotBook")]
    public class SlotBookController : Controller
    {
        // GET: api/SlotBook
        [HttpGet]
        [Route("api/SlotBook/GetUserList/{id}")]
        public string GetUserList(string id)
        {
            var SlotList = MongoDBHelper.GetEntityList<Slot>();
            var ParkingInformation = MongoDBHelper.GetEntityList<CarPark>();
           // var ParkingInformation = MongoDBHelper.SearchByObjectID<CarPark>(id);
            var customerList = MongoDBHelper.GetEntityList<Customer>();
            var res = from s in SlotList
                      join c in customerList on s.cust_id equals c._id
                      join p in ParkingInformation on s.car_park_id equals p._id
                      select new
                      {
                          //ParkingInformation.name,
                          //ParkingInformation.aspaces,
                          //ParkingInformation.tspaces,
                          p._id,
                          s.SlotNumber,
                          c.FirstName,
                          c.LastName,
                          s.vehicle_no,
                          s.Level,
                          c.Email,
                          c.Phone,
                          s.SlotStatus
                      };

            List<OccupiedSlots> slotDetails = new List<OccupiedSlots>();
            foreach (var i in res)
            {
                slotDetails.Add(new OccupiedSlots(i._id,i.SlotNumber, i.FirstName, i.LastName, i.vehicle_no, i.Level, i.Email, i.Phone, i.SlotStatus));   
            }
            //
            var Json = JsonConvert.SerializeObject(slotDetails);
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
        public string Post([FromBody]Slot value)
        {
            //Update carpark location
            AuthResponse objResponse = new AuthResponse();
            objResponse.Status = 2;                         //Success
            try
            { 
                CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(value.car_park_id);
                if (location != null)
                {
                    if (location.aspaces > 0 && location.aspaces < location.tspaces)
                    {
                        var query = Query.And(Query.EQ("car_park_id", value.car_park_id), Query.EQ("slot_status", "Empty"));
                        Slot slot = MongoDBHelper.SearchByQueryObject<Slot>(query, "Slot");
                        //Slot slot = MongoDBHelper.GetEntityList
                        if (slot != null)
                        {
                            slot.SlotStatus = "Booked";
                            slot.loc = location.name;
                            objResponse.classobject = slot;
                            MongoDBHelper.InsertEntity<Slot>(slot);

                            location.aspaces = location.aspaces - 1;
                            MongoDBHelper.InsertEntity<CarPark>(location);
                        }
                        else
                            objResponse.Status = 3;          //No slots available
                    }
                    else
                    {
                        objResponse.Status = 3;     //No Available Slots
                    }
                    
                }

                
            }
            catch(System.Exception)
            {
                //Error while processing 
                objResponse.Status = 1;
            }

            return JsonConvert.SerializeObject(objResponse);
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
