using Moq;
using NUnit.Framework;
using ProductDao;
using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using ShopifyProductsApi.Models;
using ProductDao.Contracts;
using ProductService.Contracts;
using ProductDao.Implementations;

namespace ShopifyWebDeveloperChallenge.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
       IUnitOfWork uow;
        protected Mock<IProductService> productService;
        protected Mock<IProductDao> productDao;
        protected Mock<IOrderDao> orderDao;
        protected Mock<IShopDao> shopDao;
        protected Mock<IUserDao> userDao;
        protected Mock<ILineItemDao> lineItemDao;
        protected Mock<DbContext> dbContext;

        [SetUp]
        public void Setup()
        {
            
            productService = new Mock<IProductService>();
            productDao = new Mock<IProductDao>();
            orderDao = new Mock<IOrderDao>();
            shopDao = new Mock<IShopDao>();
            userDao = new Mock<IUserDao>();
            lineItemDao = new Mock<ILineItemDao>();
            dbContext = new Mock<DbContext>();
        }
        [Test]
        public void GetAll_ShouldReturnAllProducts()
        {
            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            uow = new UnitOfWork(dbContext.Object, productDao.Object, orderDao.Object, shopDao.Object, userDao.Object, lineItemDao.Object);

            var svc = new ProductService.Implementations.ProductService(uow);
            svc.Insert(new Product
            {
                Name = "Test product",
                Value = 933.44m,
                Description = "Some product"
            });
            var allProducts = svc.GetAll();

            Assert.IsNotNull(allProducts);
            Assert.AreEqual(allProducts.Count(), 1);
        }
    }
}
