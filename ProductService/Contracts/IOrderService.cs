using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopifyProducts.Core.Implementations;

namespace ProductService.Contracts
{
    public interface IOrderService
    {
        string GenerateUniqueOrderCode(string username);
        void Save(Order order);
        IEnumerable<Order> GetAll();        
        void AddItemToOrder(long orderId, long productId, int quantity);
        void RemoveItemFromOrder(long orderId, long productId, int quantity);
        Order GetById(long id);
        void remove(long id);
    }
}
