namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel4 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookISBNs", "ISBNId", "dbo.Book");
            DropIndex("dbo.BookISBNs", new[] { "ISBNId" });
            DropTable("dbo.BookISBNs");
        }
    }
}
