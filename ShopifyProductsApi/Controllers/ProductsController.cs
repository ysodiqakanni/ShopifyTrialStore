using ShopifyProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductDao.Contracts;
using ProductService.Contracts;
using ShopifyProducts.Core.Implementations;
using System.Threading.Tasks;
using ShopifyProductsApi.ViewModels;
using System.Configuration;

namespace ShopifyProductsApi.Controllers
{
    /// <summary>
    /// This controller handles the CRUD operations for products. User authorization is required to perform the Save, Update and delete. 
    /// A required authorization code is saved under AppSettings in the web.config file
    /// </summary>
    [RoutePrefix("api/Products")]    
    public class ProductsController : ApiController
    {
        IProductService productService;
        IUserService userService;

        public ProductsController(IProductService _productService, IUserService _userService)
        {
            productService = _productService;
            userService = _userService;
        }

        // GET api/Products  
        //[ResponseType(typeof(IEnumerable<Product>))]        
        public async Task<IEnumerable<object>> GetProducts()
        {
            // return await Task.Run(() => productService.GetAll().Select( k => new Product { Name = k.Name, Value = k.Value }));
            var theProducts = await Task.Run(() => productService.GetAll());
            var data = theProducts.Select(k => new { Name = k.Name, Description = k.Description, Value = "$"+ k.Value });
            return data;
        }

        // GET api/Products/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            try
            {
                var product = productService.GetById(id);
                var data = new
                {
                    Name = product.Name,
                    Description = product.Description,
                    Value = "$" + product.Value
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                // log exception
                return BadRequest("An error occured!");
            }
        }

        // POST api/Products/Save
        /// <summary>
        /// it is assumed that Only specific authenticated users with a particular authorization code can add products. Please check the web config file for the authorizationCode to use
        /// </summary>
        /// <param name="data"></param>
        /// <example>
        /// {
        ///  "Username": "ysodiqakanni@gmail.com",
        ///  "Password": "sample String 2",
        ///  "AuthorizationCode":"Ahjd76353(8ASNDH736--087",
        /// "Product":{
        /// 	"Name":"shoe",
        /// 	"Value":43.56,
        /// 	"Description": "Some shoe"
        /// }
        ///}
        /// </example>
        /// <returns>IHttpActionResult with the data saved if successful</returns>
        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save([FromBody] AddProductViewModel data)
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
                        productService.Insert(data.Product);
                        result = Ok("Product successfuly added");
                    }
                    else
                    {
                        result = Unauthorized();
                    }
                  
                }
                catch (Exception ex)
                {
                    // Todo: log the exception
                    result = BadRequest("An error occured while processing your request: "+ ex.Message);
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
        public IHttpActionResult Update( [FromBody] AddProductViewModel data, long id)
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
                        productService.Update(id, data.Product);
                        result = Ok("Product successfuly updated");
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
                result = BadRequest("Invalid data supplied! Note that username, password, product name and description are required!");
            }

            return result;
        }

        // POST api/Products/Delete/7
        /// <summary>
        /// Please pass in the authorization data, key-value pairs of Username, Password and AuthorizationCode
        /// </summary>
        /// <param name="AuthorizationData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete([FromBody] Dictionary<string, string> AuthorizationData, long id)
        {
            IHttpActionResult response = null;
            try
            {
                string username = AuthorizationData["Username"]?? null;
                string password = AuthorizationData["Password"] ?? null;
                string authCode = AuthorizationData["AuthorizationCode"]?? null;
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
                        productService.remove(id);
                        response = Ok("Product successfuly removed!");
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
