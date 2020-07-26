using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;

namespace Result_Management_System
{
    public partial class Manage_Units : System.Web.UI.Page
    {
        MySqlConnection conn;
        String connString = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        String queryStr;
        DataSet dataset;
        String name;

        protected void Page_Load(object sender, EventArgs e)
        {
            name = (String)(Session["useremail"]);
            
            if (name == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("Login.aspx", false);
            }
            else if ((Int32)(Session["usertype"]) != 0)
            {
                Response.BufferOutput = true;
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                userLabel.Text = name;

                if (!IsPostBack)
                {
                    gridViewBind();
                }
            }
        }

        protected void gridViewBind()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "SELECT * FROM resultsmanagementsystem.units";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            dataset = new DataSet();
            adapter.Fill(dataset);
            conn.Close();

            if (dataset.Tables[0].Rows.Count > 0)
            {
                unitsGridView.DataSource = dataset;
                unitsGridView.DataBind();
            }
            else
            {
                dataset.Tables[0].Rows.Add(dataset.Tables[0].NewRow());
                unitsGridView.DataSource = dataset;
                unitsGridView.DataBind();

                int columnCount = unitsGridView.Rows[0].Cells.Count;
                unitsGridView.Rows[0].Cells.Clear();
                unitsGridView.Rows[0].Cells.Add(new TableCell());
                unitsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                unitsGridView.Rows[0].Cells[0].Text = "No Records Found";
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

        protected void addUnitEventMethod(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                addUnit();
            }
        }

        private bool ValidateInput()
        {
            bool status = true;

            if (InputValidation.ValidateUserInput(unitcodetextbox.Text) == false)
            {
                status = false;
                unitcodemessage.Text = "Unit Code is required";
                unitcodemessage.ForeColor = Color.Red;
            }
            else if (InputValidation.ValidateUnitCode(unitcodetextbox.Text) == false)
            {
                status = false;
                unitcodemessage.Text = "Unit Code must be 7 characters in the format AAA1111";
                unitcodemessage.ForeColor = Color.Red;
            }

            if (InputValidation.ValidateUserInput(unittitletextbox.Text) == false)
            {
                status = false;
                unittitlemessage.Text = "Unit Title is required";
                unittitlemessage.ForeColor = Color.Red;
            }

            if (InputValidation.ValidateUserInput(unitcoordinatortexttox.Text) == false)
            {
                status = false;
                unitcoordinatormessage.Text = "Unit Coordinator is required";
                unitcoordinatormessage.ForeColor = Color.Red;
            }

            if (InputValidation.ValidateUnitOutline(unitoutlinefileupload) == false)
            {
                status = false;
                unitoutlinemessage.Text = "Please select a PDF file";
                unitoutlinemessage.ForeColor = Color.Red;
            }

            return status;
        }

        private void addUnit()
        {
            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();

                string filepath = "/Unit_Outlines/";

                if (unitoutlinefileupload.HasFile)
                {
                    filepath += unitoutlinefileupload.FileName;
                    unitoutlinefileupload.SaveAs(MapPath(filepath));
                }

                queryStr = "INSERT INTO resultsmanagementsystem.units (unitCode, unitTitle, unitCoordinator, unitOutline) VALUES(?unitcode, ?unittitle, ?unitcoordinator, ?unitoutline)";
                cmd = new MySqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("?unitcode", unitcodetextbox.Text.ToUpper());
                cmd.Parameters.AddWithValue("?unittitle", unittitletextbox.Text);
                cmd.Parameters.AddWithValue("?unitcoordinator", unitcoordinatortexttox.Text);
                cmd.Parameters.AddWithValue("?unitoutline", filepath);
                cmd.ExecuteReader();
                conn.Close();
                resultLabel.Text = "Record Added";
                resultLabel.ForeColor = Color.Green;
                gridViewBind();
                emptyItems();
            }
            catch (Exception e)
            {
                resultLabel.Text = e.Message;
                resultLabel.ForeColor = Color.Red;
            }
        }

        protected void uGV_PageIndexChangingEvent(object sender, GridViewPageEventArgs e)
        {
            unitsGridView.PageIndex = e.NewPageIndex;
            gridViewBind();
        }

        protected void uGV_RowCancelingEditEvent(object sender, GridViewCancelEditEventArgs e)
        {
            unitsGridView.EditIndex = -1;
            gridViewBind();
        }

        protected void uGV_RowDeletingEvent(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)unitsGridView.Rows[e.RowIndex];
            string unitCode = unitsGridView.DataKeys[row.RowIndex].Value.ToString();
            Label currentUnitOutlineLabel = (Label)row.FindControl("currentUnitOutlineLabel");

            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "DELETE FROM resultsmanagementsystem.units WHERE unitCode=?unitcode";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?unitcode", unitCode);
            cmd.ExecuteNonQuery();
            conn.Close();
            deleteUnitOutline(currentUnitOutlineLabel.Text);
            gridViewBind();
        }

        protected void uGV_RowEditingEvent(object sender, GridViewEditEventArgs e)
        {
            unitsGridView.EditIndex = e.NewEditIndex;
            gridViewBind();
        }

        protected void uGV_RowUpdatingEvent(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)unitsGridView.Rows[e.RowIndex];
            string unitCode = unitsGridView.DataKeys[e.RowIndex].Value.ToString();
            TextBox unitTitleTextBox = (TextBox)row.FindControl("unitTitleTextBox");
            TextBox unitCoordinatorTextBox = (TextBox)row.FindControl("unitCoordinatorTextBox");
            FileUpload unitOutlineFileUpload = (FileUpload)row.FindControl("unitOutlineFileUpload");
            Label currentUnitOutlineLabel = (Label)row.FindControl("currentUnitOutlineLabel");

            string filepath = "/Unit_Outlines/";

            if (unitOutlineFileUpload.HasFile)
            {
                filepath += unitOutlineFileUpload.FileName;
                unitOutlineFileUpload.SaveAs(MapPath(filepath));

                if (currentUnitOutlineLabel.Text != null)
                {
                    deleteUnitOutline(currentUnitOutlineLabel.Text);
                }
            }
            else
            {
                filepath = currentUnitOutlineLabel.Text;
            }

            unitsGridView.EditIndex = -1;

            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "UPDATE resultsmanagementsystem.units SET unitTitle=?unittitle, unitCoordinator=?unitcoordinator, unitOutline=?unitoutline WHERE unitCode=?unitcode";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?unittitle", unitTitleTextBox.Text);
            cmd.Parameters.AddWithValue("?unitcoordinator", unitCoordinatorTextBox.Text);
            cmd.Parameters.AddWithValue("?unitoutline", filepath);
            cmd.Parameters.AddWithValue("?unitcode", unitCode);
            cmd.ExecuteNonQuery();
            conn.Close();
            resultLabel.Text = "Record Updated";
            resultLabel.ForeColor = Color.Green;
            gridViewBind();
        }

        protected void downloadUnitOutline(object sender, EventArgs e)
        {
            LinkButton unitOutlineButton = sender as LinkButton;
            GridViewRow row = unitOutlineButton.NamingContainer as GridViewRow;
            Label currentUnitOutlineLabel = (Label)row.FindControl("currentUnitOutlineLabel");

            string filepath = currentUnitOutlineLabel.Text;
            string filename = Path.GetFileName(filepath);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile(Server.MapPath(filepath));
            Response.End();
        }

        protected void deleteUnitOutline(string filepath)
        {
            FileInfo unitOutline = new FileInfo(Server.MapPath(filepath));

            if (unitOutline.Exists)
            {
                unitOutline.Delete();
                resultLabel.Text = "Record Deleted";
                resultLabel.ForeColor = Color.Red;
            }
            else
            {
                resultLabel.Text = "Record Deleted but could not delete Unit Outline due to non existence of file";
                resultLabel.ForeColor = Color.Red;
            }
        }

        private void emptyItems()
        {
            unitcodetextbox.Text = string.Empty;
            unitcodemessage.Text = string.Empty;
            unittitletextbox.Text = string.Empty;
            unittitlemessage.Text = string.Empty;
            unitcoordinatortexttox.Text = string.Empty;
            unitcoordinatormessage.Text = string.Empty;
            unitoutlinemessage.Text = string.Empty;
        }
    }
}