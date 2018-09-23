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
    public class UserDao : CoreDao<User>, IUserDao
    {
        public UserDao(DbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
