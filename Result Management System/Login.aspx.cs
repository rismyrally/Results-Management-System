using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Result_Management_System
{
    public partial class Login : System.Web.UI.Page
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;
        String queryStr;
        int usertype;
        String name;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginEventMethod(object sender, EventArgs e)
        {
            DoSQLQuery();
        }

        private void DoSQLQuery()
        {
            try
            {
                String connString = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ToString();
                conn = new MySqlConnection(connString);
                conn.Open();
                
                queryStr = "SELECT * FROM resultsmanagementsystem.users WHERE userEmail=?uemail AND userPassword=?upassword";
                cmd = new MySqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("?uemail", useremailTextBox.Text);
                cmd.Parameters.AddWithValue("?upassword", passwordTextBox.Text);
                reader = cmd.ExecuteReader();

                usertype = -1;
                name = "";

                while (reader.HasRows && reader.Read())
                {
                    usertype = reader.GetInt32(reader.GetOrdinal("userType"));
                    name = reader.GetString(reader.GetOrdinal("userEmail"));
                }

                if (reader.HasRows)
                {
                    if (usertype == 0)
                    {
                        Session["usertype"] = usertype;
                        Session["useremail"] = name;
                        Response.BufferOutput = true;
                        Response.Redirect("ManageUnits.aspx", false);
                    }
                    else if (usertype == 1)
                    {
                        Session["usertype"] = usertype;
                        Session["useremail"] = name;
                        Response.BufferOutput = true;
                        Response.Redirect("ManagerMenu.aspx", false);
                    }
                }
                else
                {
                    errorMessageLabel.Text = "Invalid Credentials";
                }

                reader.Close();
                conn.Close();
            }
            catch(Exception e)
            {
                errorMessageLabel.Text = e.Message;
            }
        }
    }
}