using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Result_Management_System
{
    public partial class ManageResults : System.Web.UI.Page
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
                    loadUnitssDropDownList();
                    gridViewBind();
                }
            }
        }

        protected void gridViewBind()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "SELECT r.unitCode, r.studentID, s.studentPhoto, ys.year, ys.semester, r.assessment1Score, r.assessment2Score, r.examScore, r.assessment1Score + r.assessment2Score + r.examScore AS unitScore FROM resultsmanagementsystem.students s JOIN resultsmanagementsystem.results r ON r.studentID = s.studentID JOIN resultsmanagementsystem.semester ys ON ys.semesterID = r.semesterID";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            dataset = new DataSet();
            adapter.Fill(dataset);
            conn.Close();

            if (dataset.Tables[0].Rows.Count > 0)
            {
                resultsGridView.DataSource = dataset;
                resultsGridView.DataBind();
            }
            else
            {
                dataset.Tables[0].Rows.Add(dataset.Tables[0].NewRow());
                resultsGridView.DataSource = dataset;
                resultsGridView.DataBind();

                int columnCount = resultsGridView.Rows[0].Cells.Count;
                resultsGridView.Rows[0].Cells.Clear();
                resultsGridView.Rows[0].Cells.Add(new TableCell());
                resultsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                resultsGridView.Rows[0].Cells[0].Text = "No Records Found";
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

        protected void addResultEventMethod(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                addResult();
            }
        }

        private bool ValidateInput()
        {
            bool status = true;

            if (InputValidation.ValidateUserInput(assessment1scoretextbox.Text) == false)
            {
                status = false;
                assessment1scoremessage.Text = "Assessment 1 Score is required";
                assessment1scoremessage.ForeColor = Color.Red;
            }
            else if (InputValidation.ValidateAssessmentScore(assessment1scoretextbox.Text) == false)
            {
                status = false;
                assessment1scoremessage.Text = "Assessment 1 Score should be between 0 to 20";
                assessment1scoremessage.ForeColor = Color.Red;
            }

            if (InputValidation.ValidateUserInput(assessment2scoretextbox.Text) == false)
            {
                status = false;
                assessment2scoremessage.Text = "Assessment 2 Score is required";
                assessment2scoremessage.ForeColor = Color.Red;
            }
            else if (InputValidation.ValidateAssessmentScore(assessment2scoretextbox.Text) == false)
            {
                status = false;
                assessment2scoremessage.Text = "Assessment 2 Score should be between 0 to 20";
                assessment2scoremessage.ForeColor = Color.Red;
            }

            if (InputValidation.ValidateUserInput(examscoretextbox.Text) == false)
            {
                status = false;
                examscoremessage.Text = "Exam Score is required";
                examscoremessage.ForeColor = Color.Red;
            }
            else if (InputValidation.ValidateExamScore(examscoretextbox.Text) == false)
            {
                status = false;
                examscoremessage.Text = "Exam Score should be between 0 to 60";
                examscoremessage.ForeColor = Color.Red;
            }

            return status;
        }

        private void addResult()
        {
            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();

                queryStr = "INSERT INTO resultsmanagementsystem.results (studentID, unitCode, semesterID, assessment1Score, assessment2Score, examScore) VALUES (?studentid, ?unitcode, (SELECT semesterID FROM resultsmanagementsystem.semester WHERE year=?year AND semester=?semester), ?assessment1score, ?assessment2score, ?examscore)";
                cmd = new MySqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("?studentid", studentiddropdownlist.SelectedItem.Value);
                cmd.Parameters.AddWithValue("?unitcode", unitcodedropdownlist.SelectedItem.Value);
                cmd.Parameters.AddWithValue("?year", yeardropdownlist.SelectedItem.Value);
                cmd.Parameters.AddWithValue("?semester", semesterdropdownlist.SelectedItem.Value);
                cmd.Parameters.AddWithValue("?assessment1score", assessment1scoretextbox.Text);
                cmd.Parameters.AddWithValue("?assessment2score", assessment2scoretextbox.Text);
                cmd.Parameters.AddWithValue("?examscore", examscoretextbox.Text);
                cmd.ExecuteReader();
                conn.Close();
                resultLabel.Text = "Record Added";
                resultLabel.ForeColor = Color.Green;
                gridViewBind();
                unitcodedropdownlist.SelectedIndex = 0;
                studentiddropdownlist.SelectedIndex = 0;
                studentiddropdownlist.Enabled = false;
                disableItems();
            }
            catch (Exception e)
            {
                resultLabel.Text = e.Message;
                resultLabel.ForeColor = Color.Red;
            }
        }

        protected void uGV_PageIndexChangingEvent(object sender, GridViewPageEventArgs e)
        {
            resultsGridView.PageIndex = e.NewPageIndex;
            gridViewBind();
        }

        protected void uGV_RowCancelingEditEvent(object sender, GridViewCancelEditEventArgs e)
        {
            resultsGridView.EditIndex = -1;
            gridViewBind();
        }

        protected void uGV_RowDeletingEvent(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)resultsGridView.Rows[e.RowIndex];
            string studentID = resultsGridView.DataKeys[row.RowIndex].Values["studentID"].ToString();
            string unitCode = resultsGridView.DataKeys[row.RowIndex].Values["unitCode"].ToString();
            Label year = (Label)row.FindControl("yearLabel");
            Label semester = (Label)row.FindControl("semesterLabel");

            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "DELETE FROM resultsmanagementsystem.results WHERE (unitCode=?unitcode) AND (studentID=?studentid) AND (semesterID=(SELECT semesterID FROM resultsmanagementsystem.semester WHERE year=?year AND semester=?semester))";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?studentid", studentID);
            cmd.Parameters.AddWithValue("?unitcode", unitCode);
            cmd.Parameters.AddWithValue("?year", year.Text);
            cmd.Parameters.AddWithValue("?semester", semester.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            resultLabel.Text = "Record Deleted";
            resultLabel.ForeColor = Color.Red;
            gridViewBind();
        }

        protected void uGV_RowEditingEvent(object sender, GridViewEditEventArgs e)
        {
            resultsGridView.EditIndex = e.NewEditIndex;
            gridViewBind();
        }

        protected void uGV_RowUpdatingEvent(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)resultsGridView.Rows[e.RowIndex];
            string studentID = resultsGridView.DataKeys[row.RowIndex].Values["studentID"].ToString();
            string unitCode = resultsGridView.DataKeys[row.RowIndex].Values["unitCode"].ToString();
            Label year = (Label)row.FindControl("yearLabel");
            Label semester = (Label)row.FindControl("semesterLabel");
            TextBox assessment1Score = (TextBox)row.FindControl("assessment1ScoreTextBox");
            TextBox assessment2Score = (TextBox)row.FindControl("assessment2ScoreTextBox");
            TextBox examScore = (TextBox)row.FindControl("examScoreTextBox");

            resultsGridView.EditIndex = -1;

            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "UPDATE resultsmanagementsystem.results SET assessment1Score=?assessment1score, assessment2Score=?assessment2score, examScore=?examscore WHERE (studentID=?studentid) AND (unitCode=?unitcode) AND (semesterID=(SELECT semesterID FROM resultsmanagementsystem.semester WHERE year=?year AND semester=?semester))";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?assessment1score", assessment1Score.Text);
            cmd.Parameters.AddWithValue("?assessment2score", assessment2Score.Text);
            cmd.Parameters.AddWithValue("?examscore", examScore.Text);
            cmd.Parameters.AddWithValue("?studentid", studentID);
            cmd.Parameters.AddWithValue("?unitcode", unitCode);
            cmd.Parameters.AddWithValue("?year", year.Text);
            cmd.Parameters.AddWithValue("?semester", semester.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            resultLabel.Text = "Record Updated";
            resultLabel.ForeColor = Color.Green;
            gridViewBind();
        }

        private void enableItems()
        {
            yeardropdownlist.Enabled = true;
            semesterdropdownlist.Enabled = true;
            assessment1scoretextbox.Enabled = true;
            assessment2scoretextbox.Enabled = true;
            examscoretextbox.Enabled = true;
            addresultbutton.Enabled = true;
        }

        private void disableItems()
        {
            yeardropdownlist.Items.Clear();
            yeardropdownlist.Enabled = false;
            semesterdropdownlist.Items.Clear();
            semesterdropdownlist.Enabled = false;
            assessment1scoretextbox.Text = string.Empty;
            assessment1scoretextbox.Enabled = false;
            assessment1scoremessage.Text = string.Empty;
            assessment2scoretextbox.Text = string.Empty;
            assessment2scoretextbox.Enabled = false;
            assessment2scoremessage.Text = string.Empty;
            examscoretextbox.Text = string.Empty;
            examscoretextbox.Enabled = false;
            examscoremessage.Text = string.Empty;
            addresultbutton.Enabled = false;
        }

        protected void unitSelectedEventMethod(object sender, EventArgs e)
        {
            if (unitcodedropdownlist.SelectedValue == "0")
            {
                studentiddropdownlist.Enabled = false;
            }
            else
            {
                loadStudentsDropDownList();
                studentiddropdownlist.Enabled = true;
            }
        }

        protected void studentSelectedEventMethod(object sender, EventArgs e)
        {
            if (studentiddropdownlist.SelectedValue == "0")
            {
                disableItems();
            }
            else
            {
                loadYear();
                loadSemester();
                enableItems();
            }
        }

        private void loadUnitssDropDownList()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "SELECT unitCode, unitTitle FROM resultsmanagementsystem.units";
            cmd = new MySqlCommand(queryStr, conn);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            unitcodedropdownlist.DataSource = datatable;
            unitcodedropdownlist.DataTextField = "unitTitle";
            unitcodedropdownlist.DataValueField = "unitCode";
            unitcodedropdownlist.DataBind();
        }

        private void loadStudentsDropDownList()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "SELECT DISTINCT s.studentID, CONCAT(s.studentID, ' - ', s.studentFirstName, ' ', s.studentLastName) AS student FROM resultsmanagementsystem.students AS s JOIN resultsmanagementsystem.enrollment AS e ON s.studentID = e.studentID WHERE e.unitCode=?unitcode";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?unitcode", unitcodedropdownlist.SelectedItem.Value);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            studentiddropdownlist.Items.Clear();

            if (datatable.Rows.Count > 0)
            {
                studentiddropdownlist.DataSource = datatable;
                studentiddropdownlist.DataTextField = "student";
                studentiddropdownlist.DataValueField = "studentID";
                studentiddropdownlist.DataBind();
                studentiddropdownlist.Items.Insert(0, new ListItem("<Select Student>", "0"));
            }
            else
            {
                studentiddropdownlist.Items.Insert(0, new ListItem("<No Students Enrolled>", "0"));
            }
        }

        private void loadYear()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "SELECT DISTINCT sy.year FROM resultsmanagementsystem.enrollment e JOIN resultsmanagementsystem.semester sy ON sy.semesterID = e.semesterID WHERE e.unitCode=?unitcode AND e.studentID=?studentid";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?unitcode", unitcodedropdownlist.SelectedItem.Value);
            cmd.Parameters.AddWithValue("?studentid", studentiddropdownlist.SelectedItem.Value);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            yeardropdownlist.Items.Clear();
            yeardropdownlist.DataSource = datatable;
            yeardropdownlist.DataTextField = "year";
            yeardropdownlist.DataValueField = "year";
            yeardropdownlist.DataBind();
        }

        private void loadSemester()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            queryStr = "SELECT DISTINCT sy.semester FROM resultsmanagementsystem.enrollment e JOIN resultsmanagementsystem.semester sy ON sy.semesterID = e.semesterID WHERE e.unitCode=?unitcode AND e.studentID=?studentid";
            cmd = new MySqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("?unitcode", unitcodedropdownlist.SelectedItem.Value);
            cmd.Parameters.AddWithValue("?studentid", studentiddropdownlist.SelectedItem.Value);
            adapter = new MySqlDataAdapter(cmd);

            datatable = new DataTable();
            adapter.Fill(datatable);
            conn.Close();

            semesterdropdownlist.Items.Clear();
            semesterdropdownlist.DataSource = datatable;
            semesterdropdownlist.DataTextField = "semester";
            semesterdropdownlist.DataValueField = "semester";
            semesterdropdownlist.DataBind();
        }
    }
}