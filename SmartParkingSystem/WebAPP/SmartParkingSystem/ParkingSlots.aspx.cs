using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartParkingSystem.Models;

namespace SmartParkingSystem
{
    public partial class ParkingSlots : System.Web.UI.Page
    {
        /*
        private string selectedParkingSpaceId;
        ParkingService parkingService = new ParkingService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ParkingSpaceId"] != null)
                selectedParkingSpaceId = Convert.ToString(Request.QueryString["ParkingSpaceId"]); //Send this value to DB and fetch the parking slots of this parking space.
            else
                Response.Redirect("ParkingSpaces.aspx"); //Redirect to Parking Spaces page if we are landing on this page from Home page.
                                                         //Query: Check if no parking space is selected, do we need to display the total slots of all parking spaces?
            if (!IsPostBack)
            {
                CarPark carparkObj = GetParkingSlotsSummaryFromAPI(selectedParkingSpaceId);
                ParkingSpaceLbl.Text = string.Format(ParkingSpaceLbl.Text, carparkObj.Name);
                TotalParkingSlotsLbl.Text = string.Format(TotalParkingSlotsLbl.Text, carparkObj.Tspaces);
                OccupiedParkingSlotsLbl.Text = string.Format(OccupiedParkingSlotsLbl.Text, carparkObj.Ospaces);
                AvailableParkingSlotsLbl.Text = string.Format(AvailableParkingSlotsLbl.Text, carparkObj.Aspaces);
            }
        }

        protected void TotalParkingSlots_ServerClick(object sender, EventArgs e)
        {
            ParkingDetailsGrid.DataSource = GetParkingSlotsDetailsFromAPI(selectedParkingSpaceId);
            ParkingDetailsGrid.DataBind();
        }

        protected void OccupiedParkingSlots_ServerClick(object sender, EventArgs e)
        {
            List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(selectedParkingSpaceId);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => !string.IsNullOrEmpty(s.SlotStatus) && s.SlotStatus.Equals("Parked")).ToList();
                ParkingDetailsGrid.DataBind();
            }
        }

        protected void AvailableParkingSlots_ServerClick(object sender, EventArgs e)
        {
            List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(selectedParkingSpaceId);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => string.IsNullOrEmpty(s.SlotStatus)).ToList();
                ParkingDetailsGrid.DataBind();
            }
        }

        protected void ParkingDetailsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ParkingDetailsGrid.DataSource = GetParkingSlotsDetailsFromAPI(selectedParkingSpaceId);
            ParkingDetailsGrid.PageIndex = e.NewPageIndex;
            ParkingDetailsGrid.DataBind();
        }

        private CarPark GetParkingSlotsSummaryFromAPI(string parkingSpaceId)
        {
            return parkingService.GetParkingSlotsSummaryFromAPI(parkingSpaceId);
        }

        private List<CustomerSlot> GetParkingSlotsDetailsFromAPI(string parkingSpaceId)
        {            
            return parkingService.GetParkingSlotsDetailsFromAPI(parkingSpaceId);
        }
        */
    }
}