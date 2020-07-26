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

namespace Result_Management_System
{
    public partial class ViewReports : System.Web.UI.Page
    {
        MySqlConnection conn;
        String connString = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        String queryStr;
        DataSet dataset;
        DataTable datatable;
        String name;

        protected void Page_Load(object sender, EventArgs e)
        {
            name = (String)(Session["useremail"]);

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
            else
            {
                userLabel.Text = name;

                if (!IsPostBack)
                {
                    populateDropDownLists();
                }
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

        private void populateDropDownLists()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            // populating students
            queryStr = "SELECT studentID FROM resultsmanagementsystem.students";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);

            studentIDList.DataSource = datatable;
            studentIDList.DataTextField = "studentID";
            studentIDList.DataValueField = "studentID";
            studentIDList.DataBind();

            // populating units
            queryStr = "SELECT unitCode FROM resultsmanagementsystem.units";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            unitCodeList.DataSource = datatable;
            unitCodeList.DataTextField = "unitCode";
            unitCodeList.DataValueField = "unitCode";
            unitCodeList.DataBind();

            // populating year
            queryStr = "SELECT DISTINCT year FROM resultsmanagementsystem.semester";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            yearList.DataSource = datatable;
            yearList.DataTextField = "year";
            yearList.DataValueField = "year";
            yearList.DataBind();

            // populating semester
            queryStr = "SELECT DISTINCT semester FROM resultsmanagementsystem.semester";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            semesterList.DataSource = datatable;
            semesterList.DataTextField = "semester";
            semesterList.DataValueField = "semester";
            semesterList.DataBind();
        }

        protected void searchEventMethod(object sender, EventArgs e)
        {
            string studentID = studentIDList.SelectedItem.Value;
            string unitCode = unitCodeList.SelectedItem.Value;
            string year = yearList.SelectedItem.Value;
            string semester = semesterList.SelectedItem.Value;

            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();

                queryStr = "SELECT r.studentID, r.unitCode, ys.year, ys.semester, r.assessment1Score, r.assessment2Score, r.examScore, r.assessment1Score + r.assessment2Score + r.examScore AS unitScore, " +
                            "CASE " +
                                "WHEN (r.assessment1Score + r.assessment2Score + r.examScore) >= 0 AND (r.assessment1Score + r.assessment2Score + r.examScore) < 50 THEN 'F' " +
                                "WHEN (r.assessment1Score + r.assessment2Score + r.examScore) >= 50 AND (r.assessment1Score + r.assessment2Score + r.examScore) < 60 THEN 'P' " +
                                "WHEN (r.assessment1Score + r.assessment2Score + r.examScore) >= 60 AND (r.assessment1Score + r.assessment2Score + r.examScore) < 70 THEN 'CR' " +
                                "WHEN (r.assessment1Score + r.assessment2Score + r.examScore) >= 70 AND (r.assessment1Score + r.assessment2Score + r.examScore) < 80 THEN 'D' " +
                                "ELSE 'HD' " +
                            "END AS grade, " +
                            "s.studentPhoto, u.unitOutline " +
                            "FROM resultsmanagementsystem.students s " +
                            "JOIN resultsmanagementsystem.results r " +
                            "ON s.studentID = r.studentID " +
                            "JOIN resultsmanagementsystem.units u " +
                            "ON u.unitCode = r.unitCode " +
                            "JOIN resultsmanagementsystem.semester ys " +
                            "ON ys.semesterID = r.semesterID " +
                            "WHERE r.studentID = @StudentID OR r.unitCode = @UnitCode OR ys.year = @Year OR ys.semester = @Semester " +
                            "ORDER BY r.studentID, r.unitCode";

                //queryStr = "ResultSearch";
                cmd = new MySqlCommand(queryStr, conn);

                //cmd.CommandType = CommandType.StoredProcedure;

                if (studentID != "0")
                {
                    cmd.Parameters.Add(new MySqlParameter("@StudentID", studentID));
                }
                else
                {
                    cmd.Parameters.Add(new MySqlParameter("@StudentID", ""));
                }

                if (unitCode != "0")
                {
                    cmd.Parameters.Add(new MySqlParameter("@UnitCode", unitCode));
                }
                else
                {
                    cmd.Parameters.Add(new MySqlParameter("@UnitCode", ""));
                }

                if (year != "0")
                {
                    cmd.Parameters.Add(new MySqlParameter("@Year", year));
                }
                else
                {
                    cmd.Parameters.Add(new MySqlParameter("@Year", ""));
                }

                if (semester != "0")
                {
                    cmd.Parameters.Add(new MySqlParameter("@Semester", semester));
                }
                else
                {
                    cmd.Parameters.Add(new MySqlParameter("@Semester", ""));
                }

                adapter = new MySqlDataAdapter(cmd);
                dataset = new DataSet();

                adapter.Fill(dataset);
                conn.Close();

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    reportGridView.DataSource = dataset;
                    reportGridView.DataBind();
                }
                else
                {
                    dataset.Tables[0].Rows.Add(dataset.Tables[0].NewRow());
                    reportGridView.DataSource = dataset;
                    reportGridView.DataBind();

                    int columnCount = reportGridView.Rows[0].Cells.Count;
                    reportGridView.Rows[0].Cells.Clear();
                    reportGridView.Rows[0].Cells.Add(new TableCell());
                    reportGridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                    reportGridView.Rows[0].Cells[0].Text = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                resultLabel.Text = ex.Message;
            }
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
    }
}