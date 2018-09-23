using ProductService.Contracts;
using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopifyProductsApi.Controllers
{
    /// <summary>
    /// Assumptions made as as follows:
    /// An anonymous user can create and update orders, authentication is only required at checkout point.
    /// When a LineItem is created, an order is automaticaly created with the LineItem
    /// For an anonymous user, the other is created and saved along with a Unique code and username saved as "anonymous" until the user logs in
    /// </summary>
    [RoutePrefix("api/Orders")]    
    public class OrdersController : ApiController
    {
        IOrderService orderService;
        IUserService userService;
        public OrdersController(IOrderService _orderService, IUserService _userService)
        {
            orderService = _orderService;
            userService = _userService;
        }
        // GET api/Orders  
        public async Task<IEnumerable<object>> GetAllOrders()
        {
            // return await Task.Run(() => orderService.GetAll());
            var allOthers = await Task.Run(() => orderService.GetAll());
            var data = allOthers.Select(k => new { Username = k.Username, UniqueCode = k.UniqueCode, LineItems = k.LineItems.Select(l => new {Product = l.Product.Name, Quantity = l.Quantity, Value = l.Value}) });
            return data;
        }
        // GET api/Orders/2
        public IHttpActionResult ViewOrderDetails(long orderId)
        {
            var theOrder = orderService.GetById(orderId);
            if (theOrder == null)
                return Ok(default(Order));
            var data = new
            {
                Username = theOrder.Username,
                Value = theOrder.TotalValue,
                LineItems = theOrder.LineItems.Select(l => new { Product = l.Product.Name, Quantity = l.Quantity, Value = l.Value })
            };
            return Ok(data);            
        }

        [HttpPost]
        [Route("Create/{username}")]
        public IHttpActionResult CreateOrder([FromBody] List<LineItem> LineItems, string username = null)
        {
            IHttpActionResult result = null;
            if(LineItems == null || !LineItems.Any())
            {
                result = BadRequest("An empty order cannot be saved!");
            }
            else
            {
                try
                {
                    Order order = new Order
                    {
                        Username = username ?? "Anonymous",
                        LineItems = LineItems,
                        UniqueCode = orderService.GenerateUniqueOrderCode(username)
                    };
                    orderService.Save(order);
                    result = Ok("Order created!");
                }
                catch (Exception)
                {
                    result = InternalServerError(new Exception("An error occured while creating order"));
                }
            }
            return result;
        }

        // GET api/AddItem/2/24
        // GET api/AddItem/1/-5
        [Route("AddItem/{orderId}/{productId}/{quantity}")]
        public IHttpActionResult AddOrRemoveOrderItem(long orderId, long productId, int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    // removal of item
                    orderService.RemoveItemFromOrder(orderId, productId, quantity * -1);
                    return Ok("Item removed!");
                }
                else
                {
                    orderService.AddItemToOrder(orderId, productId, quantity);
                    return Ok("Item added!");
                }                
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete([FromBody] Dictionary<string, string> AuthorizationData, long id)
        {
            IHttpActionResult response = null;
            try
            {
                string username = AuthorizationData["Username"] ?? null;
                string password = AuthorizationData["Password"] ?? null;
                string authCode = AuthorizationData["AuthorizationCode"] ?? null;
                if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(authCode))
                {
                    response = InternalServerError(new Exception("Please supply the username, pasword and authorizationCode in the Body data"));
                }
                else
                {
                    string authorizationCode = ConfigurationManager.AppSettings["authorizationCode"];
                    // authenticate and authorize user
                    if (userService.IsAuthenticated(username, password) && String.Compare(authCode, authorizationCode, true) == 0)
                    {
                        orderService.remove(id);
                        response = Ok("Order successfuly removed!");
                    }
                    else
                    {
                        response = Unauthorized();
                    }

                }
            }
            catch (Exception ex)
            {
                // log exception
                response = BadRequest("An error just occured!");
            }
            return response;
        }
    }
}
