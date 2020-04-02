using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELearnApplication.Models
{
    public enum Gennder { Male, Female }
    public enum ActiveStatus { Yes, No }
    public enum RoleTypes { User=2,Vendor}
    

    public class User
    {
        [Key]
        public string EmailId { get; set; }
        [ForeignKey("EmailId")]
        public List<User_Service> User_Services { get; set; }
        [ForeignKey("EmailId")]
        public List<Transaction> Transactions { get; set; }


        public string Name { get; set; }

        public int Age { get; set; }

        public Gennder Gennder { get; set; }

        public long ContactNumber { get; set; }

        [Column("Address")]
        public int AddressId { get; set; }

        public string Password { get; set; }

        [Column("Role")]
        public int RoleId { set; get; }

        public ActiveStatus UserStatus { get; set; }
    }

    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public List<User> Users { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int PinCode { get; set; }

    }

    public class Role
    {
        [Key]
        [Column("Code")]
        public int RoleId { set; get; }
        [ForeignKey("RoleId")]
        public List<User> Users { get; set; }

        public string Name { get; set; }
    }

    public class Service
    {
        [Key]
        [Column("Code")]
        public string ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public List<Service_Course> Service_Courses { get; set; }
        [ForeignKey("ServiceId")]
        public List<User_Service> User_Services { get; set; }
        [ForeignKey("ServiceId")]
        public List<Transaction> Transactions { get; set; }


        public string Name { get; set; }

        public int Duration { get; set; }

        public double Amount { get; set; }

        public DateTime StartDate { get; set; }


    }

    public class Course
    {
        [Key]
        [Column("Code")]
        public string CourseId { get; set; }
        [ForeignKey("CourseId")]
        public List<Service_Course> Service_Courses { get; set; }
        
        public string Name { get; set; }

        public ActiveStatus CourseStatus { get; set; }

        public string Detail { get; set; }

        [ForeignKey("CourseId")]
        public CourseRating CRating { get; set; }

    }

    public class Service_Course
    {
        [Key]
        [Column("Service",Order =1)]
        public string ServiceId { get; set; }

        [Key]
        [Column("Course", Order = 2)]
        public string CourseId { get; set; }

        public ActiveStatus Status { get; set; }
    }

    public class User_Service
    {
        [Key]
        [Column("Service", Order = 1)]
        public string ServiceId { get; set; }

        [Key]
        [Column("User", Order = 2)]
        public string EmailId { get; set; }
    }

    public class CourseRating
    {
        [Key]
        public string CourseId { get; set; }
        public string Comment { get; set; }

        
        public int UserRating { get; set; } 
        public Course Course { get; set; }
    }

    public class Notification
    {
        public int NotificationId { get; set; }
        public string NotificationMessage { get; set; }
        //Admin,User
        //course added/removed, service added/removed, user help >> Admin
        //course added/removed, service added/removed >> User
        public string NotificationFor { get; set; }

    }
}