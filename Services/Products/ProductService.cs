//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Brokers.Loggings;
using EShop.Brokers.Storages;
using EShop.Models.Shop;

namespace EShop.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly MemoryBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        public ProductService()
        {
            this.loggingBroker = new LoggingBroker();
            this.storageBroker = new MemoryBroker();
        }
        public List<Product> GetProducts()
        {
            IList<Product> products = new List<Product>();

            try
            {
                products = this.storageBroker.GetAll().ToList();
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError(exception);
            }

            if (products.Count == 0)
            {
                return new List<Product>();
            }

            return products.ToList();
        }

        public List<Product> GetAllCart()
        {
            return storageBroker.GetAllCart();
        }

        public Product Add(Product product)
        {
            if (product is null)
            {
                return new Product();
            }

            return storageBroker.Add(product);
        }

        public void AddToCart(Product product)
        {
            if (product is null)
            {
                this.loggingBroker.LogWarning("Input product is empty");
            }

            storageBroker.AddToCart(product);
        }
    }
}