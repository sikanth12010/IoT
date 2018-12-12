using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingService.Controllers
{
    [Produces("application/json")]
    [Route("api/SlotDetails")]
    public class SlotDetailsController : Controller
    {
        // GET: api/SlotDetails
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    // return new string[] { "value1", "value2" };
        //    //var filter = Builders<CarPark>.Filter.Regex("name", new BsonRegularExpression(locname, "i"));
        //    //Task<string> x = MongoDBHelper.FindCollectionByFilterAsync<CarPark>(filter);
        //    //x.Wait();
        //    //return x.Result;

        //}

        // GET: api/SlotDetails/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/SlotDetails
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/SlotDetails/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
