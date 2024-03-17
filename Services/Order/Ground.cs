//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

namespace EShop.Services.Order
{
    public class Ground : IShipping
    {
        public double GetCost(OrderService order)
        {
            if (order.GetTotal() < 100)
            {
                return 0;
            }

            return Math.Max(10, order.GetTotalWeight()*1.5);
        }

        public DateTimeOffset GetDate()
        {
            return DateTime.Now.AddDays(5);
        }
    }
}