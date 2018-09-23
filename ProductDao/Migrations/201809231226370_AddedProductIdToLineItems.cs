namespace ProductDao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductIdToLineItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LineItems", "Product_ID", "dbo.Products");
            DropIndex("dbo.LineItems", new[] { "Product_ID" });
            RenameColumn(table: "dbo.LineItems", name: "Product_ID", newName: "ProductId");
            AlterColumn("dbo.LineItems", "ProductId", c => c.Long(nullable: false));
            CreateIndex("dbo.LineItems", "ProductId");
            AddForeignKey("dbo.LineItems", "ProductId", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineItems", "ProductId", "dbo.Products");
            DropIndex("dbo.LineItems", new[] { "ProductId" });
            AlterColumn("dbo.LineItems", "ProductId", c => c.Long());
            RenameColumn(table: "dbo.LineItems", name: "ProductId", newName: "Product_ID");
            CreateIndex("dbo.LineItems", "Product_ID");
            AddForeignKey("dbo.LineItems", "Product_ID", "dbo.Products", "ID");
        }
    }
}
