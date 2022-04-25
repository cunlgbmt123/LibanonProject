namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BorrowBooks", "BUser", c => c.String());
            AddColumn("dbo.BorrowBooks", "BEmail", c => c.String());
            AddColumn("dbo.BorrowBooks", "BPhone", c => c.String());
            AlterColumn("dbo.BorrowBooks", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BorrowBooks", "Status", c => c.String());
            DropColumn("dbo.BorrowBooks", "BPhone");
            DropColumn("dbo.BorrowBooks", "BEmail");
            DropColumn("dbo.BorrowBooks", "BUser");
        }
    }
}
