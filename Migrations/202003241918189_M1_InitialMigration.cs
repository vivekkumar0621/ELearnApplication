namespace ELearnApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1_InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PinCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Gennder = c.Int(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Address = c.Int(nullable: false),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.Addresses", t => t.Address, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role, cascadeDelete: true)
                .Index(t => t.Address)
                .Index(t => t.Role);
            
            CreateTable(
                "dbo.User_Service",
                c => new
                    {
                        Service = c.String(nullable: false, maxLength: 128),
                        User = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Service, t.User })
                .ForeignKey("dbo.Users", t => t.User, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service, cascadeDelete: true)
                .Index(t => t.Service)
                .Index(t => t.User);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Service_Course",
                c => new
                    {
                        Service = c.String(nullable: false, maxLength: 128),
                        Course = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service, t.Course })
                .ForeignKey("dbo.Courses", t => t.Course, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service, cascadeDelete: true)
                .Index(t => t.Service)
                .Index(t => t.Course);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Duration = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Service", "Service", "dbo.Services");
            DropForeignKey("dbo.Service_Course", "Service", "dbo.Services");
            DropForeignKey("dbo.Users", "Role", "dbo.Roles");
            DropForeignKey("dbo.Service_Course", "Course", "dbo.Courses");
            DropForeignKey("dbo.Users", "Address", "dbo.Addresses");
            DropForeignKey("dbo.User_Service", "User", "dbo.Users");
            DropIndex("dbo.Service_Course", new[] { "Course" });
            DropIndex("dbo.Service_Course", new[] { "Service" });
            DropIndex("dbo.User_Service", new[] { "User" });
            DropIndex("dbo.User_Service", new[] { "Service" });
            DropIndex("dbo.Users", new[] { "Role" });
            DropIndex("dbo.Users", new[] { "Address" });
            DropTable("dbo.Services");
            DropTable("dbo.Roles");
            DropTable("dbo.Service_Course");
            DropTable("dbo.Courses");
            DropTable("dbo.User_Service");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
        }
    }
}
