using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopifyProductsApi.ViewModels
{
    public class AddShopViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string AuthorizationCode { get; set; }

        public Shop Shop { get; set; }
    }
}