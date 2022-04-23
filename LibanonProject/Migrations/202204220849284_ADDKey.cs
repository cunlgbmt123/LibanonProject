namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookISBNs",
                c => new
                    {
                        ISBNId = c.Int(nullable: false, identity: true),
                        ISBNcode = c.String(nullable: false, maxLength: 10),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ISBNId)
                .ForeignKey("dbo.Book", t => t.ISBNId, cascadeDelete: false)
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
                        IsBorrow = c.Boolean(nullable: false),
                        CurrentUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Users", t => t.CurrentUserId, cascadeDelete: true)
                .Index(t => t.CurrentUserId);
            
            CreateTable(
                "dbo.BorrowBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        CurrentBookId = c.Int(nullable: false),
                        CurrentUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Book", t => t.CurrentBookId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.CurrentUserId, cascadeDelete: false)
                .Index(t => t.CurrentBookId)
                .Index(t => t.CurrentUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        UserPhone = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "CurrentUserId", "dbo.Users");
            DropForeignKey("dbo.BorrowBooks", "CurrentUserId", "dbo.Users");
            DropForeignKey("dbo.BorrowBooks", "CurrentBookId", "dbo.Book");
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.BorrowBooks", new[] { "CurrentUserId" });
            DropIndex("dbo.BorrowBooks", new[] { "CurrentBookId" });
            DropIndex("dbo.Book", new[] { "CurrentUserId" });
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropTable("dbo.Users");
            DropTable("dbo.BorrowBooks");
            DropTable("dbo.Book");
            DropTable("dbo.BookISBNs");
        }
    }
}
