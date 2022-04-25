namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "OTP", c => c.String());
            AddColumn("dbo.BorrowBooks", "ActiveCode", c => c.Guid(nullable: false));
            AddColumn("dbo.BorrowBooks", "OTP", c => c.String());
            DropColumn("dbo.Users", "ActiveCode");
            DropColumn("dbo.Users", "OTP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "OTP", c => c.String());
            AddColumn("dbo.Users", "ActiveCode", c => c.Guid(nullable: false));
            DropColumn("dbo.BorrowBooks", "OTP");
            DropColumn("dbo.BorrowBooks", "ActiveCode");
            DropColumn("dbo.Book", "OTP");
        }
    }
}
