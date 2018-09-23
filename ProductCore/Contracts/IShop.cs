using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Contracts
{
    public interface IShop : IEntity
    {
        string Name { get; set; }
        string Address { get; set; }
        string ZipCode { get; set; }
        List<Product> Products { get; set; }
        List<Order> Orders { get; set; }
    }
}
