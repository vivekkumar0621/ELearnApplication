namespace ELearnApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseRatings",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        Comment = c.String(),
                        UserRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseRatings", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseRatings", new[] { "CourseId" });
            DropTable("dbo.CourseRatings");
        }
    }
}
