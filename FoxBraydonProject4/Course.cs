using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoxBraydonProject4
{
    public class Course
    {
        string days;
        int startTime;
        int endTime;
        int sectionNumber;

        public Course(string d, int start, int end, int number)
        {
            days = d;
            startTime = start;
            endTime = end;
            sectionNumber = number;
        }
    }
}