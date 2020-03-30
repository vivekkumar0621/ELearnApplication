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

    public enum RoleTypeAdmin { Admin=1}

    public class AdminSignUpDetail
    {

        [MinLength(4, ErrorMessage = "Name Should be of minimum 4 letter")]
        [MaxLength(50, ErrorMessage = "Name Should be of maximum 50 letter")]
        [Required(ErrorMessage = "Name Should be in Range 4-50")]
        public string Name { get; set; }

        [Range(18, 100, ErrorMessage = "Age Should be minimum 18")]
        [Required(ErrorMessage = "Age Field is required")]
        public int Age { get; set; }

        public Gender Gender { get; set; }

        [Range(6000000000, 9999999999, ErrorMessage = "Contact Should be of 10 digits and start with 6,7,8,9")]
        [Required(ErrorMessage = "Contact Should be of 10 digits")]
        public long ContactNumber { get; set; }

        [Key]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Address is Mandatory")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is Mandatory")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Mandatory")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is Mandatory")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Pincode is Mandatory")]
        public int PinCode { get; set; }

        [Required(ErrorMessage = "Password is Mandatory")]
        [MinLength(6, ErrorMessage = "Password should be of minimum 6 letters")]
        public string Password { get; set; }

        
     //   public RoleTypeAdmin RoleName { get; set; }

    }
}