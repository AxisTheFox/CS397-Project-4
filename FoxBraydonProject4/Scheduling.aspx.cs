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
            fillCourseDropDownList();
        }

        private void fillCourseDropDownList()
        {
            QueryController courseQueryController = new QueryController();
            courseQueryController.createDatabaseCommand("Select distinct CourseNumber from Schedule");
            courseQueryController.connectToDatabase();
            SqlDataReader reader = courseQueryController.getDatabaseCommand().ExecuteReader();
            while(reader.Read())
            {
                addCourseNumberFrom(reader);
            }
            reader.Close();
            courseQueryController.disconnectFromDatabase();
        }

        private void addCourseNumberFrom(SqlDataReader reader)
        {
            ListItem courseNumber = new ListItem(reader["CourseNumber"].ToString(), reader["CourseNumber"].ToString());
            courseNumberDropDownList.Items.Add(courseNumber);
        }
    }
}