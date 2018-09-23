
using ShopifyProducts.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Implementations
{
    public class Shop : Entity, IShop
    {
        [Required]
        [RegularExpression(@"^[ a-zA-Z0-9_]+", ErrorMessage = "Name can only contain letters, space, hyphens and numbers"), MaxLength(30, ErrorMessage = "Name should not exceed {1} characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[ a-zA-Z0-9_]+", ErrorMessage = "Address can only contain letters, space, hyphens and numbers"), MaxLength(70, ErrorMessage = "Name should not exceed {1} characters")]
        public string Address { get; set; }

        [RegularExpression(@"^[0-9_]+", ErrorMessage = "ZipCode can only contain numbers"), MaxLength(6)]
        public string ZipCode { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
