//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

namespace EShop.Services.Order
{
    public interface IShipping
    {
        double GetCost(OrderService order);
        DateTimeOffset GetDate();
    }
}