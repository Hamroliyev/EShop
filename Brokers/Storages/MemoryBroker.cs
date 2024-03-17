//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Models.Shop;

namespace EShop.Brokers.Storages
{
    public class MemoryBroker : IStorageBroker<Product>
    {
        static List<Product> products = new List<Product> {
            new Product { Name = "Banana"},
            new Product { Name = "Ananas"},
            new Product { Name = "Apple"},
            new Product { Name = "Orange"},
        };

        static List<Product> cartProducts = new List<Product>();

        public Product Add(Product product)
        {
            products.Add(product);

            return product;
        }

        public List<Product> GetAllCart()
        {
            return cartProducts;
        }

        public Product AddToCart(Product product)
        {
            cartProducts.Add(product);
            return product;
        }
        public List<Product> GetAll()
        {
            return products;
        }
    }
}