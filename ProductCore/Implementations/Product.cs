
using ShopifyProducts.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Implementations
{
    public class Product : Entity, IEntity
    {
        [Required]
        [RegularExpression(@"^[ a-zA-Z0-9_]+", ErrorMessage = "Name can only contain letters, space, hyphens and numbers"), MaxLength(30, ErrorMessage = "Name should not exceed {1} characters")]
        public string Name { get; set; }    // Product name is unique

        [Required]
        [RegularExpression(@"^[ a-zA-Z0-9_]+", ErrorMessage = "Description can only contain letters, space, hyphens and numbers"), MaxLength(300, ErrorMessage = "Description should not exceed {1} characters")]
        public string Description { get; set; }    

        [Required, DataType(DataType.Currency), Range(0.5, 500000, ErrorMessage ="Product value must be between ${1} and ${2}")]
        public decimal Value { get; set; }  // Product'a unit price

        public virtual List<LineItem> LineItems { get; set; }
       
    }
}
