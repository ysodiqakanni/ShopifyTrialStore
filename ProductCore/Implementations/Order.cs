
using ShopifyProducts.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Implementations
{
    public class Order : Entity, IOrder
    {
        public string UniqueCode { get; set; }    // ALphanumeric unique code to represent an order. 
        [Required]
        [RegularExpression(@"^[ a-zA-Z0-9_]+", ErrorMessage = "Name can only contain letters, space, hyphens and numbers"), MaxLength(30, ErrorMessage = "Name should not exceed {1} characters")]
        public string Username { get; set; }
        public decimal TotalValue
        {
            get
            {
                if (LineItems != null && LineItems.Any())
                    return LineItems.Sum(l => l.Value);
                return 0m;
            }
            set { }
        }   // sum of values of all lineItems
        public virtual List<LineItem> LineItems { get; set; }
    }
}
