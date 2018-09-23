using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Contracts
{
    public interface IShopService
    {
        IEnumerable<Shop> GetAll();
        Shop GetById(long id);
        void Insert(Shop shopDto);
        void remove(long id);
        void Update(long id, Shop shopDto);
    }
}
