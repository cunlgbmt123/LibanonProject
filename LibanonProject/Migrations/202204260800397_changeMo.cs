namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Code");
        }
    }
}
