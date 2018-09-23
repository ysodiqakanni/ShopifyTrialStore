using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using ShopifyProducts.Core.Implementations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ShopifyProductsApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    public class ProductInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            List<Shop> Shops = new List<Shop>
            {
                new Shop { ID = 1, Name = "Xtra Electronics", Address = "Times Sq NYC", ZipCode="023443", DateCreated = DateTime.Now.AddDays(-45), DateModified = DateTime.Now.AddDays(-25) },
                new Shop { ID = 2, Name = "Monica home stuffs", Address = "Grenoble", ZipCode="94632", DateCreated = DateTime.Now.AddDays(-45), DateModified = DateTime.Now.AddDays(-25) },
                new Shop { ID = 3, Name = "The rightz", Address = "Times Sq NYC", ZipCode="023443", DateCreated = DateTime.Now.AddDays(-45), DateModified = DateTime.Now.AddDays(-25) },
            };
            context.Shops.AddRange(Shops);
            List<Product> Products = new List<Product>
            {
                new Product {ID = 1, Name = "Fan", Value = 56.99m, Description="Test fan", DateCreated = DateTime.Now, DateModified= DateTime.Now },
                new Product {ID = 2, Name = "Phone", Value = 156.99m, Description="Test phone", DateCreated = DateTime.Now, DateModified= DateTime.Now },
            };
            context.Products.AddRange(Products);
            List<LineItem> LineItems = new List<LineItem>
            {
                new LineItem { ID = 1, Product = Products.Where(p=>p.ID == 1 ).FirstOrDefault(), Quantity = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now }
            };
            context.LineItems.AddRange(LineItems);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}