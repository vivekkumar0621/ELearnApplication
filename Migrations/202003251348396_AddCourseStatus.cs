namespace ELearnApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CourseStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CourseStatus");
        }
    }
}
