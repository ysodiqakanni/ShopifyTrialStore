namespace ProductDao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shops", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Shops", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shops", "Country", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Shops", "Name");
        }
    }
}
