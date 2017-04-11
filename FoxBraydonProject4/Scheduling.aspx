<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scheduling.aspx.cs" Inherits="FoxBraydonProject4.Scheduling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <h1>Schedule Courses</h1>
        
        <p>
            Course number:
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="courseNumberDropDownList" runat="server"></asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="addCourseButton" runat="server" Text="Add Course" OnClick="addCourseButton_Click" />
        </p>

        <p>
            Currently selected courses:
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="selectedCoursesLabel" runat="server" Text=""></asp:Label>
        </p>

        <p>
            Schedule slots remaining:
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="coursesRemainingLabel" runat="server" Text="4"></asp:Label>
        </p>

        <p>
            <asp:Button ID="scheduleCoursesButton" runat="server" Text="Schedule Courses" Enabled="False" />
        </p>

        <p>
            <asp:Label ID="errorLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
        </p>
    
    </div>
    </form>
</body>
</html>
