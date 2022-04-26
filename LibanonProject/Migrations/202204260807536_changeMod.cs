namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMod : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Code", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "Code", c => c.String());
        }
    }
}
