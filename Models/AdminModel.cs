using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELearnApplication.Models
{
    public class UserDetail
    {
        [Key]
        public string EmailId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public RoleType RoleName { get; set; }
        public ActiveStatus UserStatus { get; set; }


        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PinCode { get; set; }

    }

    public class UserServiceCourse
    {
        public List<UserDetail> UserDetails { get; set; }
        public List<Course> Courses { get; set; }
        public List<Service> Services { get; set; }
    }
}