//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Models.Auth;

namespace EShop.Brokers.Storages
{
    public interface IStorageBroker
    {
        List<Credential> GetAllCredentials();
        Credential AddCredential(Credential credential);
    }
}