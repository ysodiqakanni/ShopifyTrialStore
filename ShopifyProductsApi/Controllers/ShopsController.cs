using ProductService.Contracts;
using ShopifyProducts.Core.Implementations;
using ShopifyProductsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        [Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            try
            {
                var shop = shopService.GetById(id);             
                return Ok(shop);
            }
            catch (Exception ex)
            {
                // log exception
                return BadRequest("An error occured!");
            }
        }
        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save([FromBody] AddShopViewModel data)
        {
            IHttpActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    string authorizationCode = ConfigurationManager.AppSettings["authorizationCode"];
                    // authenticate and authorize user
                    if (userService.IsAuthenticated(data.Username, data.Password) && String.Compare(data.AuthorizationCode, authorizationCode, true) == 0)
                    {
                        shopService.Insert(data.Shop);
                        result = Ok("Shop successfuly added");
                    }
                    else
                    {
                        result = Unauthorized();
                    }

                }
                catch (Exception ex)
                {
                    // Todo: log the exception
                    result = BadRequest("An error occured while processing your request: " + ex.Message);
                }
            }
            else
            {
                string errorMsgs = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                result = BadRequest(errorMsgs);  // ("Invalid data supplied! Note that username, password, product name, Value and description are required!");
            }

            return result;
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IHttpActionResult Update([FromBody] AddShopViewModel data, long id)
        {
            IHttpActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    string authorizationCode = ConfigurationManager.AppSettings["authorizationCode"];
                    // authenticate and authorize user
                    if (userService.IsAuthenticated(data.Username, data.Password) && String.Compare(data.AuthorizationCode, authorizationCode, true) == 0)
                    {
                        shopService.Update(id, data.Shop);
                        result = Ok("Shop successfuly updated");
                    }
                    else
                    {
                        result = Unauthorized();
                    }

                }
                catch (Exception ex)
                {
                    // Todo: log the exception
                    result = BadRequest("An error occured while processing your request: " + ex.Message);
                }
            }
            else
            {
                result = BadRequest("Invalid data supplied!");
            }

            return result;
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
                        shopService.remove(id);
                        response = Ok("Shop successfuly removed!");
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
