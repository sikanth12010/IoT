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
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateHTMLForParkingSpace();            
        }
        
        protected void LinkBtnParkingSpaceName_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ParkingSlots.aspx?ParkingSpaceId=" + ((LinkButton)sender).CommandArgument);
            ParkingSpaceIdLbl.Value = ((LinkButton)sender).CommandArgument;
            CarPark carparkObj = GetParkingSlotsSummaryFromAPI(ParkingSpaceIdLbl.Value);
            ParkingSpaceNameLbl.Text = string.Format(ParkingSpaceNameLbl.Text, carparkObj.Name);
            TotalParkingSlotsLbl.Text = string.Format(TotalParkingSlotsLbl.Text, carparkObj.Tspaces);
            OccupiedParkingSlotsLbl.Text = string.Format(OccupiedParkingSlotsLbl.Text, carparkObj.Ospaces);
            AvailableParkingSlotsLbl.Text = string.Format(AvailableParkingSlotsLbl.Text, carparkObj.Aspaces);

            ParkingDetailsGrid.DataSource = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            ParkingDetailsGrid.DataBind();

            PnlForParkingSlots.Visible = true;
        }

        protected void TotalParkingSlots_ServerClick(object sender, EventArgs e)
        {
            ParkingDetailsGrid.DataSource = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            ParkingDetailsGrid.DataBind();
        }

        protected void OccupiedParkingSlots_ServerClick(object sender, EventArgs e)
        {
            List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => !string.IsNullOrEmpty(s.SlotStatus) && s.SlotStatus.Equals("Parked")).ToList();
                ParkingDetailsGrid.DataBind();
            }
        }

        protected void AvailableParkingSlots_ServerClick(object sender, EventArgs e)
        {
            List<CustomerSlot> parkingSlots = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            if (parkingSlots != null)
            {
                ParkingDetailsGrid.DataSource = parkingSlots.Where(s => string.IsNullOrEmpty(s.SlotStatus)).ToList();
                ParkingDetailsGrid.DataBind();
            }
        }

        protected void ParkingDetailsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ParkingDetailsGrid.DataSource = GetParkingSlotsDetailsFromAPI(ParkingSpaceIdLbl.Value);
            ParkingDetailsGrid.PageIndex = e.NewPageIndex;
            ParkingDetailsGrid.DataBind();
        }

        private void CreateHTMLForParkingSpace()
        {
            List<CarPark> parkingSpaceDetailsList = GetAllParkingSpacesOfOwnerFromAPI();
            if (parkingSpaceDetailsList != null && parkingSpaceDetailsList.Count > 0)
            {
                LinkButton linkBtnParkingSpaceName = null;
                foreach (CarPark parkingSpace in parkingSpaceDetailsList)
                {
                    linkBtnParkingSpaceName = new LinkButton();
                    linkBtnParkingSpaceName.Text = parkingSpace.Name;
                    linkBtnParkingSpaceName.CommandArgument = parkingSpace.Id;
                    linkBtnParkingSpaceName.CssClass = "col-md-2 slots totalslots";
                    linkBtnParkingSpaceName.Click += LinkBtnParkingSpaceName_Click;
                    PhForParkingSpaces.Controls.Add(linkBtnParkingSpaceName);
                }
            }
        }

        private List<CarPark> GetAllParkingSpacesOfOwnerFromAPI()
        {
            return parkingService.GetAllParkingSpacesOfOwnerFromAPI("1");            
        }

        private CarPark GetParkingSlotsSummaryFromAPI(string parkingSpaceId)
        {
            return parkingService.GetParkingSlotsSummaryFromAPI(parkingSpaceId);
        }

        private List<CustomerSlot> GetParkingSlotsDetailsFromAPI(string parkingSpaceId)
        {
            return parkingService.GetParkingSlotsDetailsFromAPI(parkingSpaceId);
        }
    }
}