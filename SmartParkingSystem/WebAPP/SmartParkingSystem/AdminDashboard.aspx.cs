using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartParkingSystem
{
    public partial class _Default : Page
    {
        private string ownerType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["OwnerType"] != null)
                {
                    ownerType = Convert.ToString(Request.QueryString["OwnerType"]);
                    if (ownerType.Equals("SuperAdmin"))
                        DivSuperAdmin.Visible = true;
                    else
                        DivSuperAdmin.Visible = false;
                }
            }
        }
    }
}