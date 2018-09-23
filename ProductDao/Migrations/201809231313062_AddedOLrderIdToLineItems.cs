namespace ProductDao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOLrderIdToLineItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LineItems", "Order_ID", "dbo.Orders");
            DropIndex("dbo.LineItems", new[] { "Order_ID" });
            RenameColumn(table: "dbo.LineItems", name: "Order_ID", newName: "OrderId");
            AlterColumn("dbo.LineItems", "OrderId", c => c.Long(nullable: false));
            CreateIndex("dbo.LineItems", "OrderId");
            AddForeignKey("dbo.LineItems", "OrderId", "dbo.Orders", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.LineItems", new[] { "OrderId" });
            AlterColumn("dbo.LineItems", "OrderId", c => c.Long());
            RenameColumn(table: "dbo.LineItems", name: "OrderId", newName: "Order_ID");
            CreateIndex("dbo.LineItems", "Order_ID");
            AddForeignKey("dbo.LineItems", "Order_ID", "dbo.Orders", "ID");
        }
    }
}
