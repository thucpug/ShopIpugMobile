namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delaliasproduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Alias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Alias", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
