namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropPrimaryKey("dbo.BookISBNs");
            AlterColumn("dbo.BookISBNs", "ISBNId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BookISBNs", "ISBNId");
            CreateIndex("dbo.BookISBNs", "ISBNId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropPrimaryKey("dbo.BookISBNs");
            AlterColumn("dbo.BookISBNs", "ISBNId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookISBNs", "ISBNId");
            CreateIndex("dbo.BookISBNs", "ISBNId");
        }
    }
}
