using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ELearnApplication.Models
{
    public class ModelContext:DbContext
    {
        public  ModelContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Service_Course> Service_Courses { get; set; }
        public DbSet<User_Service> User_Services { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

     //   public System.Data.Entity.DbSet<ELearnApplication.Models.NewPassword> NewPasswords { get; set; }

        // public System.Data.Entity.DbSet<ELearnApplication.Models.ForgetDetails> ForgetDetails { get; set; }

        //public System.Data.Entity.DbSet<ELearnApplication.Models.AdminSignUpDetail> AdminSignUpDetails { get; set; }
        //    public System.Data.Entity.DbSet<ELearnApplication.Models.UserDetail> UserDetails { get; set; }
    }
}