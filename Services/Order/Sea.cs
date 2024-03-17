//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

namespace EShop.Services.Order
{
    public class Sea : IShipping
    {
        public double GetCost(OrderService order)
        {
            if (order.GetTotal() < 100)
            {
                return 0;
            }

            return Math.Max(15, order.GetTotalWeight()*2);
        }

        public DateTimeOffset GetDate()
        {
            return DateTime.Now.AddDays(6);
        }
    }
}