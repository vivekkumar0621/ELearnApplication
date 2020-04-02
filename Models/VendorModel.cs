using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearnApplication.Models
{
    public class OnlineServices
    {
        public string ServiceName { get; set; }
        public List<Course> Courses { get; set; }
        public double Amount { get; set; }

        public List<CourseRating> UserCourseRatings { get; set; }
    }
}