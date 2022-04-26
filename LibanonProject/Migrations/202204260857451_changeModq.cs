namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeModq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false),
                        StateBorrow = c.Boolean(nullable: false),
                        StateIsBorrow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Book", t => t.StateId)
                .Index(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "StateId", "dbo.Book");
            DropIndex("dbo.States", new[] { "StateId" });
            DropTable("dbo.States");
        }
    }
}
