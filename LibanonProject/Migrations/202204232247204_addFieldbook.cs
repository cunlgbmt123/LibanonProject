namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldbook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "OwnerName", c => c.String());
            AddColumn("dbo.Book", "OwnerEmail", c => c.String());
            AddColumn("dbo.Book", "OwnerPhone", c => c.String());
            AddColumn("dbo.Book", "ActiveCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "ActiveCode");
            DropColumn("dbo.Book", "OwnerPhone");
            DropColumn("dbo.Book", "OwnerEmail");
            DropColumn("dbo.Book", "OwnerName");
        }
    }
}
