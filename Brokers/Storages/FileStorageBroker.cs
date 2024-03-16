//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using System.Collections.Generic;
using EShop.Models.Auth;

namespace EShop.Brokers.Storages
{
    public class FileStorageBroker
    {
        private const string FilePath = "Assets/Credentials.txt";

        public FileStorageBroker()
        {
            EnsureFileExists();
        }

        private static List<Credential> credentials = new List<Credential>()
        {
            new Credential { Username="shox", Password="12345" },
            new Credential { Username = "shox1", Password = "1233345" }
        };

        public List<Credential> GetAllCredentials()
        {
            List<Credential> credentialsList = new List<Credential>(); 
            string[] credentialLines = File.ReadAllLines(FilePath);
            
            foreach (string credentialLine in credentialLines)
            {
                string[] credentialProperties = credentialLine.Split('*');

                credentialsList.Add(new Credential()
                    {Username = credentialProperties[0], 
                    Password = credentialProperties[1]});
            }

            return credentialsList;
        } 
        
        public Credential AddCredential(Credential credential)
        {
            string credentialLine = $"{credential.Username}*{credential.Password}\n";
            File.AppendAllText(FilePath,credentialLine);

            return credential;
        }

        private void EnsureFileExists()
        {
            bool fileExists = File.Exists(FilePath);

            if (fileExists is false)
            {
                File.Create(FilePath).Close();
            }
        }
    }
}