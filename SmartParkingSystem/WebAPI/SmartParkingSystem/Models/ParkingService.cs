using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace SmartParkingSystem.Models
{
    public class ParkingService
    {
        const string url = "http://parkingwebapi.azurewebsites.net/api/location";
        public Owner GetOwner(string email, string password)
        {
            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);                
                var responseTask = client.GetAsync("owner");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result != null && result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Owner>();
                    readTask.Wait();
                    var owner = readTask.Result;
                    return owner;
                }
            }
            return null;*/

            Owner owner = new Owner();
            owner.FirstName = "Swati";
            owner.LastName = "Karakavalasa";
            owner.OwnerType = "Admin";
            owner.Password = "test";
            return owner;
        }

        public List<CarPark> GetAllParkingSpacesOfOwnerFromAPI(string ownerId)
        {
            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);                
                var responseTask = client.GetAsync("carpark");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result != null && result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CarPark>();
                    readTask.Wait();
                    var carpark = readTask.Result;
                    return carpark;
                }
            }
            return null;*/
            List<CarPark> parkingSpacesList = new List<CarPark>();
            CarPark carpark = new CarPark();
            carpark.Id = "1";
            carpark.Name = "Mastek Parking, Mahape";
            carpark.Tspaces = 50;
            carpark.Aspaces = 20;
            carpark.Ospaces = carpark.Tspaces - carpark.Aspaces;
            parkingSpacesList.Add(carpark);

            carpark = new CarPark();
            carpark.Id = "2";
            carpark.Name = "Rupa Solitare Parking, Mahape";
            carpark.Tspaces = 34;
            carpark.Aspaces = 8;
            carpark.Ospaces = carpark.Tspaces - carpark.Aspaces;
            parkingSpacesList.Add(carpark);
            return parkingSpacesList;
        }
        public CarPark GetParkingSlotsSummaryFromAPI(string parkingSpaceId)
        {
            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);                
                var responseTask = client.GetAsync("carpark");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result != null && result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CarPark>();
                    readTask.Wait();
                    var carpark = readTask.Result;
                    return carpark;
                }
            }
            return null;*/
            CarPark carpark = new CarPark();
            carpark.Name = "Mastek Parking, Mahape";
            carpark.Tspaces = 50;
            carpark.Aspaces = 20;
            carpark.Ospaces = carpark.Tspaces - carpark.Aspaces;
            return carpark;
        }

        public List<CustomerSlot> GetParkingSlotsDetailsFromAPI(string parkingSpaceId)
        {
            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);                
                var responseTask = client.GetAsync("slot");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result != null && result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerSlot[]>();
                    readTask.Wait();
                    var slot = readTask.Result;
                    return slot.ToList();
                }
            }
            return null;*/

            List<CustomerSlot> parkingSlotsList = new List<CustomerSlot>();
            CustomerSlot parkingSlot = new CustomerSlot();
            parkingSlot.ParkingSpaceId = parkingSpaceId;
            parkingSlot.SlotNo = "L0-1";
            parkingSlot.CustomerName = "Santosh Kumar";
            parkingSlot.VehicleNo = "MH-04-1122";
            parkingSlot.Email = "santosh.kumar@gmail.com";
            parkingSlot.Phone = "98765412345";
            parkingSlot.SlotStatus = "Parked";
            parkingSlotsList.Add(parkingSlot);

            parkingSlot = new CustomerSlot();
            parkingSlot.ParkingSpaceId = parkingSpaceId;
            parkingSlot.SlotNo = "L0-2";            
            parkingSlotsList.Add(parkingSlot);

            parkingSlot = new CustomerSlot();
            parkingSlot.ParkingSpaceId = parkingSpaceId;
            parkingSlot.SlotNo = "L0-3";
            parkingSlot.CustomerName = "John";
            parkingSlot.VehicleNo = "MH-12-3242";
            parkingSlot.Email = "john.j@gmail.com";
            parkingSlot.Phone = "91254678911";
            parkingSlot.SlotStatus = "Parked";
            parkingSlotsList.Add(parkingSlot);

            parkingSlot = new CustomerSlot();
            parkingSlot.ParkingSpaceId = "2";
            parkingSlot.SlotNo = "P2 - 1";
            parkingSlot.CustomerName = "Vicky";
            parkingSlot.VehicleNo = "MH-12-1111";
            parkingSlot.Email = "vicky.j@gmail.com";
            parkingSlot.Phone = "91334678911";
            parkingSlot.SlotStatus = "Parked";
            parkingSlotsList.Add(parkingSlot);

            return parkingSlotsList;
        }
    }
}