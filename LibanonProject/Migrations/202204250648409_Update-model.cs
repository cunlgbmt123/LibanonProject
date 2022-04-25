namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookISBNs",
                c => new
                    {
                        ISBNId = c.Int(nullable: false),
                        ISBNcode = c.String(),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ISBNId)
                .ForeignKey("dbo.Book", t => t.ISBNId)
                .Index(t => t.ISBNId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Image = c.String(),
                        Publisher = c.DateTime(nullable: false),
                        Category = c.String(),
                        Summary = c.String(),
                        BookStatus = c.Boolean(),
                        OwnerName = c.String(),
                        OwnerEmail = c.String(),
                        OwnerPhone = c.String(),
                        ActiveCode = c.Guid(nullable: false),
                        OTP = c.String(),
                        IsBorrow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.BorrowBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(),
                        BUser = c.String(),
                        BEmail = c.String(),
                        BPhone = c.String(),
                        ActiveCode = c.Guid(nullable: false),
                        OTP = c.String(),
                        CurrentBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Book", t => t.CurrentBookId, cascadeDelete: true)
                .Index(t => t.CurrentBookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowBooks", "CurrentBookId", "dbo.Book");
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.BorrowBooks", new[] { "CurrentBookId" });
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropTable("dbo.BorrowBooks");
            DropTable("dbo.Book");
            DropTable("dbo.BookISBNs");
        }
    }
}
