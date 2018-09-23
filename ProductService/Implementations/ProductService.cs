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
    public class ProductService : IProductService
    {
        IUnitOfWork uow;
        public ProductService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public IEnumerable<Product> GetAll()
        {
            var allProducts = uow.productDao.GetAll();
            return allProducts.ToList();
        }

        public Product GetById(long id)
        {
            return uow.productDao.Get(id);
        }

        public void Insert(Product productDto)
        {
            var checkProduct = uow.productDao.Find(p => String.Compare(p.Name, productDto.Name, true) == 0).FirstOrDefault();
            if (checkProduct != null)
                throw new Exception("A product with this name already exists");            
            uow.productDao.Add(productDto);
            uow.Save();
        }

        public void remove(long id)
        {
            var product = GetById(id);
            if (product == null)
                throw new Exception("Product with the id not found");
            uow.productDao.Remove(product);
            uow.Save();
        }

        public void Update(long id, Product productDto)
        {
            var productToBeUpdated = GetById(id);
            if (productToBeUpdated == null)
                throw new Exception("Product not found!");
            // check for uniqueness of product name
            if(String.Compare(productToBeUpdated.Name, productDto.Name, true) != 0)
            {
                var checkProduct = uow.productDao.Find(p => String.Compare(p.Name, productDto.Name, true) == 0).FirstOrDefault();
                if (checkProduct != null)
                    throw new Exception("A product with this name already exists");
            }
            productToBeUpdated.Name = productDto.Name;
            productToBeUpdated.Description = productDto.Description;
            productToBeUpdated.Value = productDto.Value;
            uow.productDao.Update(productToBeUpdated);
            uow.Save();
        }
    }
}
