namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategorys", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategorys", "Status");
        }
    }
}
