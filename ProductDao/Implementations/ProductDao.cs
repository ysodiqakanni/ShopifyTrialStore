using ShopifyProducts.Core.Implementations;
using ProductDao.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDao.Implementations
{
    public class ProductDao : CoreDao<Product>, IProductDao
    {
        public ProductDao(DbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
