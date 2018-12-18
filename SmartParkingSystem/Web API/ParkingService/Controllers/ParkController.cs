using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    [Produces("application/json")]
    [Route("api/SlotEmpty/")]
    public class SlotEmptyController : Controller
    {
        // GET: api/Park

        // GET: api/Park/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Park
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Park/5
        [HttpPut("{id}/{slot_id}")]
        public string Put(string id, int slot_id, [FromBody]JsonPatchDocument<Slot> slotPatch)
        {
            AuthResponse auth = new AuthResponse();
            CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(id);
            Slot slotDetails = MongoDBHelper.SearchByQueryObject<Slot>(Query.EQ("slot_no", slot_id), "Slot");
            if (location != null) // && slotDetails != null)
            {
                 if (slotDetails.SlotStatus != "Booked")
                 {
                location.aspaces = location.aspaces + 1;
                MongoDBHelper.InsertEntity<CarPark>(location);
                     slotDetails.SlotStatus = "Empty";                   //To reset the slot status
                    MongoDBHelper.InsertEntity<Slot>(slotDetails);
                }
                auth.Status = 0;
                return JsonConvert.SerializeObject(auth);
            }
            auth.Status = 1;
            return JsonConvert.SerializeObject(auth);

        }

        /*
        [HttpPatch("{carpark_id}/{slot_id}")]
        public string ClearSlot(string carpark_id, int slot_id, [FromBody] JsonPatchDocument <Slot> slotPatch )
        {
            AuthResponse auth = new AuthResponse();
            CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(carpark_id);
            Slot slotDetails = MongoDBHelper.SearchByQueryObject<Slot>(Query.EQ("slot_no", slot_id), "Slot");
            if (location != null && slotDetails != null)
            {
                 if (slotDetails.SlotStatus != "Booked")
                 {
                location.aspaces = location.aspaces + 1;
                MongoDBHelper.InsertEntity<CarPark>(location);
                     slotDetails.SlotStatus = "Empty";                   //To reset the slot status
                 }
                auth.Status = 0;
                return JsonConvert.SerializeObject(auth);
            }
            auth.Status = 1;
            return JsonConvert.SerializeObject(auth);
        }
        */
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
