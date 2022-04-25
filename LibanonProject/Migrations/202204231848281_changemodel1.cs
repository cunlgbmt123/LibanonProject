namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropPrimaryKey("dbo.BookISBNs");
            AlterColumn("dbo.BookISBNs", "ISBNId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookISBNs", "ISBNId");
            CreateIndex("dbo.BookISBNs", "ISBNId");
            AddForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book", "BookId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropPrimaryKey("dbo.BookISBNs");
            AlterColumn("dbo.BookISBNs", "ISBNId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BookISBNs", "ISBNId");
            CreateIndex("dbo.BookISBNs", "ISBNId");
            AddForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book", "BookId", cascadeDelete: true);
        }
    }
}
