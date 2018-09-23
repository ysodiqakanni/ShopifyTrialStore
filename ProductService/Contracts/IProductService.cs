using ShopifyProducts.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(long id);
        void Insert(Product productDto);
        void remove(long id);
        void Update(long id, Product product);
    }
}
