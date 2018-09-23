namespace ProductDao.Migrations
{
    using ShopifyProducts.Core.Implementations;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopifyProductsApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ShopifyProductsApi.Models.ApplicationDbContext";
        }

        protected override void Seed(ShopifyProductsApi.Models.ApplicationDbContext context)
        {

            context.Products.AddOrUpdate(
                p => p.ID,
                new Product { ID = 1, Name = "Fan", Value = 56.99m, Description = "Test fan", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                new Product { ID = 2, Name = "Phone", Value = 156.99m, Description = "Test phone", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                );
            context.Shops.AddOrUpdate(
               p => p.ID,
               new Shop { ID = 1, Name = "Xtra Electronics", Address = "Times Sq NYC", ZipCode = "023443", DateCreated = DateTime.Now.AddDays(-45), DateModified = DateTime.Now.AddDays(-25) },
                new Shop { ID = 2, Name = "Monica home stuffs", Address = "Grenoble", ZipCode = "94632", DateCreated = DateTime.Now.AddDays(-45), DateModified = DateTime.Now.AddDays(-25) },
                new Shop { ID = 3, Name = "The rightz", Address = "Times Sq NYC", ZipCode = "023443", DateCreated = DateTime.Now.AddDays(-45), DateModified = DateTime.Now.AddDays(-25) }
               );
            context.LineItems.AddOrUpdate(l => l.ID,
                new LineItem { ID = 1, ProductId = 2, OrderId = 1, Quantity = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now });
            context.Orders.AddOrUpdate(o => o.ID,
                new Order { ID = 1, UniqueCode = "ysodiqakanni~38477-242dd-24324ds-233a", Username = "ysodiqakanni", DateCreated = DateTime.Now, DateModified = DateTime.Now });
        }
    }
}
