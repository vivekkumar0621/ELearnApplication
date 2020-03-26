namespace ELearnApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetailCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Detail");
        }
    }
}
