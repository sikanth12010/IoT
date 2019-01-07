using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartParkingSystem.Models;

namespace SmartParkingSystem
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        ParkingService parkingService = new ParkingService();
        List<Owner> ownerList = new List<Owner>();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetOwnerListFromAPI();
            ManageOwnersGrid.Visible = true;
            ManageOwnersGrid.DataSource = ownerList;
            ManageOwnersGrid.DataBind();
        }

        private List<Owner> GetOwnerListFromAPI()
        {
            ownerList = parkingService.GetOwnerListFromAPI();
            return ownerList;
        }
    }
}