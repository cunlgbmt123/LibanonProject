namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeModelN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        ActiveCode = c.Guid(nullable: false),
                        OTP = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Book", t => t.UserID)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Book", "BorrowerName", c => c.String());
            AddColumn("dbo.Book", "BorrowerEmail", c => c.String());
            AddColumn("dbo.Book", "BorrowerPhone", c => c.String());
            DropColumn("dbo.Book", "OwnerName");
            DropColumn("dbo.Book", "OwnerEmail");
            DropColumn("dbo.Book", "OwnerPhone");
            DropColumn("dbo.Book", "ActiveCode");
            DropColumn("dbo.Book", "OTP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "OTP", c => c.String());
            AddColumn("dbo.Book", "ActiveCode", c => c.Guid(nullable: false));
            AddColumn("dbo.Book", "OwnerPhone", c => c.String());
            AddColumn("dbo.Book", "OwnerEmail", c => c.String());
            AddColumn("dbo.Book", "OwnerName", c => c.String());
            DropForeignKey("dbo.Users", "UserID", "dbo.Book");
            DropIndex("dbo.Users", new[] { "UserID" });
            DropColumn("dbo.Book", "BorrowerPhone");
            DropColumn("dbo.Book", "BorrowerEmail");
            DropColumn("dbo.Book", "BorrowerName");
            DropTable("dbo.Users");
        }
    }
}
