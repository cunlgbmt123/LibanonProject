namespace LibanonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel21 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.OTPViews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OTPViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OTP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
