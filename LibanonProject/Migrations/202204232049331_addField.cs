namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "BookStatus", c => c.Boolean());
            AddColumn("dbo.Users", "ActiveCode", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "OTP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "OTP");
            DropColumn("dbo.Users", "ActiveCode");
            DropColumn("dbo.Book", "BookStatus");
        }
    }
}
