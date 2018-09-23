using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDao.Contracts
{
    public interface IUnitOfWork
    {
        IProductDao productDao { get; set; }
        IOrderDao orderDao { get; set; }
        IShopDao shopDao { get; set; }
        IUserDao userDao { get; set; }
        ILineItemDao lineItemDao { get; set; }
        void Save();
    }
}
