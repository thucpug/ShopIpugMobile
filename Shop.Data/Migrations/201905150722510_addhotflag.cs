namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhotflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "HotFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "HotFlag");
        }
    }
}
