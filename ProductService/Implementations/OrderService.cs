using ProductService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopifyProducts.Core.Implementations;
using ProductDao.Contracts;

namespace ProductService.Implementations
{
    public class OrderService : IOrderService
    {
        IUnitOfWork uow;
        public OrderService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public void AddItemToOrder(long orderId, long productId, int quantity)
        {
            var order = GetById(orderId);
            if (order == null)
                throw new Exception("Invalid order ID");
            var product = uow.productDao.Get(productId);
            if (product == null)
                throw new Exception("Invalid product ID");
            var theLineItem = order.LineItems.Where(l => l.Product.ID == productId).FirstOrDefault();
            if (theLineItem == null)
            {
                LineItem lineItem = new LineItem()
                {
                    Product = product,
                    Quantity = quantity
                };
                order.LineItems.Add(lineItem);
                uow.orderDao.Update(order);
            }
            else
            {
                theLineItem.Quantity += quantity;
                uow.lineItemDao.Update(theLineItem);
            }
            uow.Save();
        }

        public void RemoveItemFromOrder(long orderId, long productId, int quantity)
        {
            var order = GetById(orderId);
            if (order == null)
                throw new Exception("Invalid order ID");
            var product = uow.productDao.Get(productId);
            if (product == null)
                throw new Exception("Invalid product ID");
            var theLineItem = order.LineItems.Where(l => l.Product.ID == productId).FirstOrDefault();
            if (theLineItem == null)
                throw new Exception("the item does not exist in the order");
            if (theLineItem.Quantity < quantity)
                throw new Exception("the quatity of items to remove is more than the number of available items");
            theLineItem.Quantity -= quantity;
            uow.lineItemDao.Update(theLineItem);
            uow.Save();
        }


        public string GenerateUniqueOrderCode(string username)
        {
            string code = $"{username}~{Guid.NewGuid()}";
            return code;
        }
        public Order GetById(long id)
        {
            return uow.orderDao.Get(id);
        }
        public IEnumerable<Order> GetAll()
        {
            var allOrders = uow.orderDao.GetAll();
            return allOrders.ToList();
        }
        public void remove(long id)
        {
            var order = GetById(id);
            if (order == null)
                throw new Exception("Order with the id not found");
            uow.orderDao.Remove(order);
            uow.Save();
        }
        public void Save(Order order)
        {            
            uow.orderDao.Add(order);
            uow.Save();
        }
    }
}
