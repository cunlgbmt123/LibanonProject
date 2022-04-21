namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datav2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropIndex("dbo.Users", new[] { "UserId" });
            DropPrimaryKey("dbo.BookISBNs");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.BookISBNs", "ISBNId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookISBNs", "ISBNId");
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.BookISBNs", "ISBNId");
            CreateIndex("dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.BookISBNs");
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.BookISBNs", "ISBNId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.BookISBNs", "ISBNId");
            CreateIndex("dbo.Users", "UserId");
            CreateIndex("dbo.BookISBNs", "ISBNId");
        }
    }
}
