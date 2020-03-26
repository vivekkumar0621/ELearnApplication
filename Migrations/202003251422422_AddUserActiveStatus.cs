namespace ELearnApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserActiveStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UsUserStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UsUserStatus");
        }
    }
}
