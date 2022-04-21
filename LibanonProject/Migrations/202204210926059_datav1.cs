namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datav1 : DbMigration
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
                .ForeignKey("dbo.Book", t => t.ISBNId, cascadeDelete: true)
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
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        UserPhone = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Book", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserId", "dbo.Book");
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropTable("dbo.Users");
            DropTable("dbo.Book");
            DropTable("dbo.BookISBNs");
        }
    }
}
