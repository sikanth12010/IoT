using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartParkingSystem.Models;

namespace SmartParkingSystem
{
    public partial class ParkingSpaces : System.Web.UI.Page
    {
        ParkingService parkingService = new ParkingService();
        List<CarPark> parkingSpaceDetailsList = new List<CarPark>();
        static List<CustomerSlot> parkingSlots = new List<CustomerSlot>();
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateHTMLForParkingSpace();
        }
        
        protected void LinkBtnParkingSpaceName_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ParkingSlots.aspx?ParkingSpaceId=" + ((LinkButton)sender).CommandArgument);
            ParkingSpaceIdLbl.Value = ((LinkButton)sender).CommandArgument;
            CarPark carparkObj = GetParkingSlotsSummaryFromAPI(ParkingSpaceIdLbl.Value);
            ParkingSpaceNameLbl.Text = string.Format("Parking Space: {0}", carparkObj.Name);
            TotalParkingSlotsLbl.Text = string.Format("Total Slots: {0}", carparkObj.Tspaces);
            OccupiedParkingSlotsLbl.Text = string.Format("Occupied Slots: {0}", carparkObj.Ospaces);
            AvailableParkingSlotsLbl.Text = string.Format("Available Slots: {0}", carparkObj.Aspaces);
           
            GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            var res = (from cs in parkingSlots
                       join cp in parkingSpaceDetailsList on cs.car_park_id equals cp._Id
                       where cs.car_park_id == carparkObj._Id && cs.SlotStatus == "Booked"
                       select cs).Count<CustomerSlot>();

            BookedParkingSlotsLbl.Text = string.Format("Booked Slots: {0}", res);

            var result = parkingSlots.Where(x => x.car_park_id == carparkObj._Id);
            List<CustomerSlot> parkDetails = new List<CustomerSlot>();
            parkDetails = result.ToList<CustomerSlot>();

            PnlForParkingSlots.Visible = true;
            if (parkDetails.Count != 0)
            {
                ParkingDetailsGrid.Visible = true;
                ParkingDetailsGrid.DataSource = parkDetails;
                ParkingDetailsGrid.DataBind();
            }
            else
            {
                ParkingDetailsGrid.Visible = false;
            }
    
        }

       

        protected void TotalParkingSlots_ServerClick(object sender, EventArgs e)
        {
            ParkingDetailsGrid.DataSource = parkingSlots;  //GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            ParkingDetailsGrid.DataBind();
        }

        protected void OccupiedParkingSlots_ServerClick(object sender, EventArgs e)
        {
            //List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => s.SlotStatus == "Parked").ToList();
                ParkingDetailsGrid.DataBind();
            }
        }

        protected void AvailableParkingSlots_ServerClick(object sender, EventArgs e)
        {
            //List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => s.SlotStatus == "Empty").ToList();
                ParkingDetailsGrid.DataBind();
            }
        }

        protected void BookedParkingSlots_ServerClick(object sender, EventArgs e)
        {
            //List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => s.SlotStatus == "Booked").ToList();
                ParkingDetailsGrid.DataBind();
            }
        } 

        protected void ParkingDetailsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ParkingDetailsGrid.DataSource = parkingSlots; // GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            ParkingDetailsGrid.PageIndex = e.NewPageIndex;
            ParkingDetailsGrid.DataBind();
        }


        private void CreateHTMLForParkingSpace()
        {
            parkingSpaceDetailsList = GetAllParkingSpacesOfOwnerFromAPI();
            if (parkingSpaceDetailsList != null && parkingSpaceDetailsList.Count > 0)
            {
                LinkButton linkBtnParkingSpaceName = null;
                int idx = 0;
                foreach (CarPark parkingSpace in parkingSpaceDetailsList)
                {
                    linkBtnParkingSpaceName = new LinkButton();
                    //linkBtnParkingSpaceName.
                    linkBtnParkingSpaceName.Text = parkingSpace.Name;
                    linkBtnParkingSpaceName.CommandArgument = idx.ToString();
                    linkBtnParkingSpaceName.CssClass = "col-md-3 slots parkslots";
                    linkBtnParkingSpaceName.Click += LinkBtnParkingSpaceName_Click;
                    PhForParkingSpaces.Controls.Add(linkBtnParkingSpaceName);
                    ++idx;
                }
            }
        }

        private List<CarPark> GetAllParkingSpacesOfOwnerFromAPI()
        {
            return parkingService.GetAllParkingSpacesOfOwnerFromAPI("1");            
        }

        private CarPark GetParkingSlotsSummaryFromAPI(string parkingSpaceId)
        {
            return parkingService.GetParkingSlotsSummaryFromAPI(parkingSpaceId, parkingSpaceDetailsList);
        }

        private List<CustomerSlot> GetParkingSlotsDetailsFromAPI(string parkingSpaceId)
        {
            parkingSlots = parkingService.GetParkingSlotsDetailsFromAPI(parkingSpaceId, parkingSpaceDetailsList);
            return parkingSlots;
        }
    }
}