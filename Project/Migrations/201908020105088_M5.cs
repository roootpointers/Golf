namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Admins", "Coins");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Coins", c => c.Double(nullable: false));
        }
    }
}
