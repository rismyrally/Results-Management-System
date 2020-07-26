using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Result_Management_System
{
    public partial class ManagerMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = (String)(Session["useremail"]);

            if (name == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("Login.aspx", false);
            }
            else if ((Int32)(Session["usertype"]) != 1)
            {
                Response.BufferOutput = true;
                Response.Redirect("Login.aspx", false);
            }
        }

        protected void logoutEventMethod(object sender, EventArgs e)
        {
            Session["usertype"] = null;
            Session["useremail"] = null;
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Login.aspx", false);
        }

        protected void manageResultsEventMethod(object sender, EventArgs e)
        {
            Response.Redirect("ManageResults.aspx", false);
        }

        protected void viewReportsEventMethod(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx", false);
        }
    }
}