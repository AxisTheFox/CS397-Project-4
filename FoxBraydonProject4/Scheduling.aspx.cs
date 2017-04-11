using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoxBraydonProject4
{
    public partial class Scheduling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["numberOfAddedCourses"] = 1;
                fillCourseDropDownList();
            }
        }

        private void fillCourseDropDownList()
        {
            QueryController courseListQueryController = new QueryController();
            courseListQueryController.createDatabaseCommand("Select distinct CourseNumber from Schedule");
            courseListQueryController.connectToDatabase();
            SqlDataReader reader = courseListQueryController.getDatabaseCommand().ExecuteReader();
            while(reader.Read())
            {
                addCourseNumberFrom(reader);
            }
            reader.Close();
            courseListQueryController.disconnectFromDatabase();
        }

        private void addCourseNumberFrom(SqlDataReader reader)
        {
            ListItem courseNumber = new ListItem(reader["CourseNumber"].ToString(), reader["CourseNumber"].ToString());
            courseNumberDropDownList.Items.Add(courseNumber);
        }

        protected void addCourseButton_Click(object sender, EventArgs e)
        {
            bool coursePreviouslyAdded = false;

            for (int i = 1; i < Int32.Parse(Session["numberOfAddedCourses"].ToString()); i++)
            {
                if (courseWasAlreadyAdded(i))
                {
                    errorLabel.Text = "You may not add any course more than once.";
                    coursePreviouslyAdded = true;
                }
            }
            if (!coursePreviouslyAdded)
            {
                errorLabel.Text = "";
                if (userHasNotAddedTooManyCourses())
                {
                    fillCourseSessionVariables();
                }
            }
        }

        private bool courseWasAlreadyAdded(int i)
        {
            return courseNumberDropDownList.SelectedValue.ToString().Equals(Session["course" + i].ToString());
        }

        private bool userHasNotAddedTooManyCourses()
        {
            return Int32.Parse(Session["numberOfAddedCourses"].ToString()) <= 4;
        }

        private void fillCourseSessionVariables()
        {
            string course = "course" + Session["numberOfAddedCourses"].ToString();
            QueryController qc = new QueryController();
            qc.createDatabaseCommand("Select * from Schedule where CourseNumber=@cn");
            qc.addQueryParameter("@cn", courseNumberDropDownList.SelectedValue.ToString());
            Session[course] = courseNumberDropDownList.SelectedValue.ToString();
            Session[course + "referenceNumbers"] = "";
            Session[course + "courseNumbers"] = "";
            Session[course + "sections"] = "";
            Session[course + "days"] = "";
            Session[course + "startTimes"] = "";
            Session[course + "endTimes"] = "";
            qc.connectToDatabase();
            SqlDataReader reader = qc.getDatabaseCommand().ExecuteReader();
            while (reader.Read())
            {
                Session[course + "crns"] = Session[course + "crns"] + reader["CRN"].ToString() + ",";
                Session[course + "courseNumbers"] = Session[course + "courseNumbers"] + reader["CourseNumber"].ToString() + ",";
                Session[course + "sectionNumbers"] = Session[course + "sectionNumbers"] + reader["SectionNumber"].ToString() + ",";
                Session[course + "days"] = Session[course + "days"] + reader["Days"].ToString() + ",";
                Session[course + "startTimes"] = Session[course + "startTimes"] + reader["StartTime"].ToString() + ",";
                Session[course + "endTimes"] = Session[course + "endTimes"] + reader["EndTime"].ToString() + ",";
            }
            reader.Close();
            qc.disconnectFromDatabase();
            Session["numberOfAddedCourses"] = Int32.Parse(Session["numberOfAddedCourses"].ToString()) + 1;
            addToSelectedCoursesLabel(course);
            coursesRemainingLabel.Text = (5 - Int32.Parse(Session["numberOfAddedCourses"].ToString())).ToString();
            if(coursesRemainingLabel.Text.Equals("0"))
            {
                scheduleCoursesButton.Enabled = true;
                addCourseButton.Enabled = false;
            }
        }

        private void addToSelectedCoursesLabel(string course)
        {
            if (Session["numberOfAddedCourses"].ToString().Equals("2"))
            {
                selectedCoursesLabel.Text += Session[course].ToString();
            }
            else
            {
                selectedCoursesLabel.Text += ", " + Session[course].ToString();
            }
        }
    }
}