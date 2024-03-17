//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Models.Auth;

namespace EShop.Brokers.Storages
{
    public interface IStorageBroker<T> where T : class
    {
        List<T> GetAll();
        T Add(T credential);
    }
}