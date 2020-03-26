namespace ELearnApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUserActiveStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "UsUserStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UsUserStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "UserStatus");
        }
    }
}
