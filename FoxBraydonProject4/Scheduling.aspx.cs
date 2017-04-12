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
        string course1;
        string course2;
        string course3;
        string course4;

        string[] course1Crns;
        string[] course2Crns;
        string[] course3Crns;
        string[] course4Crns;

        string[] course1CourseNumbers;
        string[] course2CourseNumbers;
        string[] course3CourseNumbers;
        string[] course4CourseNumbers;

        string[] course1Sections;
        string[] course2Sections;
        string[] course3Sections;
        string[] course4Sections;

        string[] course1Days;
        string[] course2Days;
        string[] course3Days;
        string[] course4Days;

        string[] course1StartTimes;
        string[] course2StartTimes;
        string[] course3StartTimes;
        string[] course4StartTimes;

        string[] course1EndTimes;
        string[] course2EndTimes;
        string[] course3EndTimes;
        string[] course4EndTimes;

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
            Session[course + "crns"] = "";
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

        protected void scheduleCoursesButton_Click(object sender, EventArgs e)
        {
            setCourses();
            setCourseCrns();
            setCourseNumbers();
            setCourseSections();
            setCourseDays();
            setCourseStartTimes();
            setCourseEndTimes();

            scheduleCoursesFromBeginning();
        }

        private void setCourses()
        {
            course1 = Session["course1"].ToString();
            course2 = Session["course2"].ToString();
            course3 = Session["course3"].ToString();
            course4 = Session["course4"].ToString();
        }

        private void setCourseCrns()
        {
            course1Crns = Session["course1crns"].ToString().Split(',');
            course2Crns = Session["course2crns"].ToString().Split(',');
            course3Crns = Session["course3crns"].ToString().Split(',');
            course4Crns = Session["course4crns"].ToString().Split(',');
        }

        private void setCourseNumbers()
        {
            course1CourseNumbers = Session["course1courseNumbers"].ToString().Split(',');
            course2CourseNumbers = Session["course2courseNumbers"].ToString().Split(',');
            course3CourseNumbers = Session["course3courseNumbers"].ToString().Split(',');
            course4CourseNumbers = Session["course4courseNumbers"].ToString().Split(',');
        }

        private void setCourseSections()
        {
            course1Sections = Session["course1sectionNumbers"].ToString().Split(',');
            course2Sections = Session["course2sectionNumbers"].ToString().Split(',');
            course3Sections = Session["course3sectionNumbers"].ToString().Split(',');
            course4Sections = Session["course4sectionNumbers"].ToString().Split(',');
        }

        private void setCourseDays()
        {
            course1Days = Session["course1days"].ToString().Split(',');
            course2Days = Session["course2days"].ToString().Split(',');
            course3Days = Session["course3days"].ToString().Split(',');
            course4Days = Session["course4days"].ToString().Split(',');
        }

        private void setCourseStartTimes()
        {
            course1StartTimes = Session["course1startTimes"].ToString().Split(',');
            course2StartTimes = Session["course2startTimes"].ToString().Split(',');
            course3StartTimes = Session["course3startTimes"].ToString().Split(',');
            course4StartTimes = Session["course4startTimes"].ToString().Split(',');
        }

        private void setCourseEndTimes()
        {
            course1EndTimes = Session["course1EndTimes"].ToString().Split(',');
            course2EndTimes = Session["course2EndTimes"].ToString().Split(',');
            course3EndTimes = Session["course3EndTimes"].ToString().Split(',');
            course4EndTimes = Session["course4EndTimes"].ToString().Split(',');
        }

        private void scheduleCoursesFromBeginning()
        {

        }
    }
}