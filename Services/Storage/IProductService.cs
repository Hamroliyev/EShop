//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Models.Shop;

namespace EShop.Services.Storage
{
    public interface IProductService
    {
        List<Product> GetProducts();
        void AddToCart(Product product);
    }
}