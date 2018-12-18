using System;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using SmartParkingSystem.Models;

namespace SmartParkingSystem.Account
{
    public partial class Login : Page
    {
        ParkingService parkingService = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            parkingService = new ParkingService();
            RegisterHyperLink.NavigateUrl = "Register";
            ResetPasswordHyperLink.NavigateUrl = "ResetPassword";
            ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //Validate the admin user and password
                //Check the user type : admin/super user
                //Redirect to the respective page as per the user type
                var owner = parkingService.GetOwner(Email.Text, Password.Text);
                if (owner != null && owner.Status == 0)
                {

                    if (!string.IsNullOrEmpty(owner.Password) && owner.Password.Equals(Password.Text))
                    {
                        switch (owner.OwnerType)
                        {
                            case "Admin":
                            case "SuperAdmin":
                                Session["LoggedInUser"] = owner;
                                Response.Redirect("~/AdminDashboard?OwnerType=" + owner.OwnerType);
                                break;
                            default:
                                FailureText.Text = "Invalid login attempt";
                                ErrorMessage.Visible = true;
                                break;
                        }
                    }
                }
                else
                {
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                }

                //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                //var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);


                /*switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }*/
            }
        }

        private void GetLoggedInUserDetails()
        {
            
        }
    }
}