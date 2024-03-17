//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Brokers.Storages;
using EShop.Models.Shop;

namespace EShop.Services.Storage
{
    public class ProductService : IProductService
    {
        private readonly MemoryBroker storageBroker;
        public ProductService()
        {
            storageBroker = new MemoryBroker();
        }
        public List<Product> GetProducts()
        {
            return storageBroker.GetAll();
        }

        public List<Product> GetAllCart()
        {
            return storageBroker.GetAllCart();
        }

        public Product Add(Product product)
        {
            return storageBroker.Add(product);
        }

        public void AddToCart(Product product)
        {
            storageBroker.AddToCart(product);
        }
    }
}