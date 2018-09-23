using ProductDao.Contracts;
using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDao.Implementations
{    
    public class ShopDao : CoreDao<Shop>, IShopDao
    {
        public ShopDao(DbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
