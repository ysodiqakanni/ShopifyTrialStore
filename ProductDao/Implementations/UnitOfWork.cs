using ProductDao.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDao.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private System.Data.Entity.DbContext context;// = new DbContext();

        //ICompanyRepository gcompanyRepository;   
        public UnitOfWork(System.Data.Entity.DbContext _context, IProductDao _productDao, IOrderDao _orderDao, IShopDao _shopDao, IUserDao _userDao , ILineItemDao _lineItemDao)
        {
     
            context = _context;
            productDao = _productDao;
            orderDao = _orderDao;
            shopDao = _shopDao;
            lineItemDao = _lineItemDao;
            userDao = _userDao;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public IProductDao productDao
        {
            get; set;
        }
        public IOrderDao orderDao { get; set; }
        public IShopDao shopDao { get; set; }
        public IUserDao userDao { get; set; }
        public ILineItemDao lineItemDao { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
   
}
