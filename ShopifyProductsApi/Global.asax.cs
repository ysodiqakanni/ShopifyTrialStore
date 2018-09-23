using ProductDao.Contracts;
using ProductDao.Implementations;
using ProductService.Contracts;
using ShopifyProductsApi.Models;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector.Integration.Web;
using System.Reflection;

namespace ShopifyProductsApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Database.SetInitializer(new ProductInitializer());

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();



            container.Register<System.Data.Entity.DbContext>(() => new ApplicationDbContext(), Lifestyle.Scoped);

            // register the DAOs
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IProductDao, ProductDao.Implementations.ProductDao>(Lifestyle.Scoped);
            container.Register<IShopDao, ProductDao.Implementations.ShopDao>(Lifestyle.Scoped);
            container.Register<IOrderDao, ProductDao.Implementations.OrderDao>(Lifestyle.Scoped);
            container.Register<ILineItemDao, ProductDao.Implementations.LineItemDao>(Lifestyle.Scoped);
            container.Register<IUserDao, ProductDao.Implementations.UserDao>(Lifestyle.Scoped);

            // register the services
            container.Register<IProductService, ProductService.Implementations.ProductService>(Lifestyle.Scoped);
            container.Register<IUserService, ProductService.Implementations.UserService>(Lifestyle.Scoped);
            container.Register<IOrderService, ProductService.Implementations.OrderService>(Lifestyle.Scoped);
            container.Register<IShopService, ProductService.Implementations.ShopService>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
