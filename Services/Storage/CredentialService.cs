//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Brokers.Loggings;
using EShop.Brokers.Storages;
using EShop.Models.Auth;

namespace EShop.Services.Storage
{
    public class CredentialService : ICredentialService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CredentialService()
        {
            this.storageBroker = new FileStorageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public Credential AddCredential(Credential credential)
        {
            return credential is null
                ? CreateAndLogInvalidCredential()
                : ValidateAndAddCredential(credential);
        }

        public List<Credential> GetAllCredentials()
        {
            if (this.storageBroker.GetAllCredentials().Count == 0)
            {
                return new List<Credential>();
            }

            return this.storageBroker.GetAllCredentials();
        }

        private Credential CreateAndLogInvalidCredential()
        {
            this.loggingBroker.LogError("Contact is invalid");

            return new Credential();
        }

        private Credential ValidateAndAddCredential(Credential credential)
        {
            if ( String.IsNullOrWhiteSpace(credential.Username)
                || String.IsNullOrWhiteSpace(credential.Password))
            {
                this.loggingBroker.LogError("Contact details missing.");

                return new Credential();
            }
            else
            {
                return this.storageBroker.AddCredential(credential);
            }
        }
    }
}