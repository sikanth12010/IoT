using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingService.Controllers;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ParkingService.Test
{
    [TestClass]
    public class Park
    {
        [TestMethod]
        public void UpdatePark()
        {
            using (var client = new HttpClient())
            {
                //api/park/{id}/put client.PutAsJsonAsync("api/article/2/put", articleModel);
                var jsonString = "{\"id\":5abcde4d47235c2dfc7022dc,\"aspaces\":1}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                       // client.  PutAsJsonAsync("api/article/2/put", articleModel);
                var message = client.PutAsync(new Uri("http://localhost/ParkingService/api/park/2/put"), httpContent);

                //Assert.IsFalse(result, $"{value} should not be prime");
                //Assert.AreEqual(HttpStatusCode.NoContent, message.sta);
            }
        }
    }
}
