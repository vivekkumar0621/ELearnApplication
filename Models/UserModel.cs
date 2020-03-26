using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELearnApplication.Models
{
    public partial class UserHome
    {
        public string Id { get; set; }
        public String CourseCode { get; set; }
        public String CourseName { get; set; }
        public String Servicename { get; set; }
        public String StartDate { get; set; }
        public string EndDate { get; set; }
        public String Detail { get; set; }
    }
}