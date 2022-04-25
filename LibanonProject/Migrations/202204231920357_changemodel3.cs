namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropTable("dbo.BookISBNs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookISBNs",
                c => new
                    {
                        ISBNId = c.Int(nullable: false),
                        ISBNcode = c.String(),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ISBNId);
            
            CreateIndex("dbo.BookISBNs", "ISBNId");
            AddForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book", "BookId");
        }
    }
}
