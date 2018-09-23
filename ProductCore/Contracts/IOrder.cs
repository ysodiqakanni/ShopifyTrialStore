using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Contracts
{
    public interface IOrder : IEntity
    {
        string UniqueCode { get; set; }
        string Username { get; set; }
        decimal TotalValue { get; set; }
        List<LineItem> LineItems { get; set; }
    }
}
