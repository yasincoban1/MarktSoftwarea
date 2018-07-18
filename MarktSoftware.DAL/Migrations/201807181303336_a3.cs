namespace MarktSoftware.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OldPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "NewHot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NewHot");
            DropColumn("dbo.Products", "OldPrice");
        }
    }
}
