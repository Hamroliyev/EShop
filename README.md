# EShop
This development is about to get shopping.

The app has several use-cases such as auhtentication service,
shipping service, and user-readble console ui.

## Auhtentication

1. We have FileStorageBroker which works on credentials.
you can use it for add credential to the file  and get all credentials 
from the file, and it inherit from IStorageBroker.cs interface.

    FileStorageBroker.cs

        //----------------------------------------
        // Tarteeb School (c) All rights reserved
        //----------------------------------------

        using EShop.Models.Auth;

        namespace EShop.Brokers.Storages
        {
            public class FileStorageBroker : IStorageBroker<Credential>
            {
                private const string FilePath = "Assets/Credentials.txt";

                public FileStorageBroker()
                {
                    EnsureFileExists();
                }

                public List<Credential> GetAll()
                {
                    List<Credential> credentials = new List<Credential>(); 
                    string[] credentialLines = File.ReadAllLines(FilePath);
                    
                    foreach (string credentialLine in credentialLines)
                    {
                        string[] credentialProperties = credentialLine.Split('*');

                        credentials.Add(new Credential()
                            {Username = credentialProperties[0], 
                            Password = credentialProperties[1]});
                    }

                    return credentials;
                } 
                
                public Credential Add(Credential credential)
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
2. We have also MemoryBroker inherits from IStorageBroker.cs interface. The broker work with products and use in-memory for storing products.

    MemoryBroker.cs

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

---

## Logging

Main is logging broker. The broker manage to log on console. It has four essential methods.

    LoggingBroker.cs

        //----------------------------------------
        // Tarteeb School (c) All rights reserved
        //----------------------------------------

        namespace EShop.Brokers.Loggings
        {
            public class LoggingBroker : ILoggingBroker
            {
                public void LogError(Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception.Message);
                    Console.ResetColor();
                }

                public void LogError(string message)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }

                public void LogInformation(string message)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }

                public void LogWarning(string message)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
            }
        }