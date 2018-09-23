using ProductService.Contracts;
using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShopifyProductsApi.Controllers
{

    [RoutePrefix("api/Shops")]
    public class ShopsController : ApiController
    {
        IUserService userService;
        IShopService shopService;
        public ShopsController(IShopService _shopService, IUserService _userService)
        {
            shopService = _shopService;
            userService = _userService;
        }

        // GET api/Shops  
        [ResponseType(typeof(IEnumerable<Shop>))]
        public async Task<IEnumerable<Shop>> GetShops()
        {
            return await Task.Run(() => shopService.GetAll());
        }
    }
}
