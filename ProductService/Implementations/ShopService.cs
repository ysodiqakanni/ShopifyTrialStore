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
    public class ShopService : IShopService
    {
        IUnitOfWork uow;
        public ShopService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public IEnumerable<Shop> GetAll()
        {
            var allShops = uow.shopDao.GetAll();
            return allShops.ToList();
        }

        public Shop GetById(long id)
        {
            return uow.shopDao.Get(id);
        }

        public void Insert(Shop shopDto)
        {
            var checkShop = uow.shopDao.Find(s => String.Compare(s.Name, shopDto.Name, true) == 0).FirstOrDefault();
            if (checkShop != null)
                throw new Exception("A shop with this name already exists");
            uow.shopDao.Add(shopDto);
            uow.Save();
        }

        public void remove(long id)
        {
            var shop = GetById(id);
            if (shop == null)
                throw new Exception("Shop with the id not found");
            uow.shopDao.Remove(shop);
            uow.Save();
        }

        public void Update(long id, Shop shopDto)
        {
            var shopToBeUpdated = GetById(id);
            if (shopToBeUpdated == null)
                throw new Exception("Product not found!");
            // check for uniqueness of shop name
            if (String.Compare(shopToBeUpdated.Name, shopDto.Name, true) != 0)
            {
                var checkShop = uow.shopDao.Find(p => String.Compare(p.Name, shopDto.Name, true) == 0).FirstOrDefault();
                if (checkShop != null)
                    throw new Exception("A shop with this name already exists");
            }
            shopToBeUpdated.Name = shopDto.Name;
            shopToBeUpdated.Address = shopDto.Address;
            shopToBeUpdated.ZipCode = shopDto.ZipCode;
            uow.shopDao.Update(shopToBeUpdated);
            uow.Save();
        }
    }
}
