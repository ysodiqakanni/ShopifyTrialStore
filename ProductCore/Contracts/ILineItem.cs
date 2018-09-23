using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Contracts
{
    public interface ILineItem : IEntity
    {
        decimal Value { get; set; }
         int Quantity { get; set; }
         Product Product { get; set; }
        long ProductId { get; set; }
    }
}
