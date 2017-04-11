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

        //public bool overlaps(Section otherSection)
        //{
        //    // check days
        //    // if days are different, return false
        //    // if at least one day is common, check times
        //    // return either true or false.
        //}
    }
}