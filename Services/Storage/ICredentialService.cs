//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Models.Auth;

namespace EShop.Services.Storage
{
    public interface ICredentialService
    {
        List<Credential> GetAllCredentials();
        Credential AddCredential(Credential credential);
    }
}