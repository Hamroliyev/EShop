# EShop
This development is about to get shopping.

The app has several use-cases such as auhtentication service,
shipping service, and user-readble console ui.

## Auhtentication

1. We have FileStorageBroker which works on credentials.
you can use it for add credential to the file  and get all credentials 
from the file, and it inherit from IStorageBroker.cs interface.

    Model

        namespace EShop.Models.Auth
        {
            public class Credential
            {
                public string Username { get; set; }
                public string Password { get; set; }
            }
        }

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

    LoginService.cs

        using EShop.Brokers.Storages;
        using EShop.Models.Auth;

        namespace EShop.Services.Login  
        {
            public class LoginService : ILoginService
            {
                private readonly IStorageBroker<Credential> storageBroker;

                public LoginService()
                {
                    this.storageBroker = new FileStorageBroker();
                }

                public bool CheckUserLogin(Credential credential)
                {
                    foreach (Credential credentialItem in storageBroker.GetAll())
                    {
                        if (credential.Username == credentialItem.Username && 
                            credential.Password == credentialItem.Password)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

2. We have also MemoryBroker inherits from IStorageBroker.cs interface. The broker work with products and use in-memory for storing products.

    MemoryBroker.cs

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


## Order

Model

    namespace EShop.Models.Shop
    {
        public class Product
        {
            public string Name { get; set; }
            public double Weight { get; set; }
        }
    }
 
We have some sort of shipping types and you can use all of that you want.

1.Sea
1.Air
1.Ground 
and many more

-Air

    namespace EShop.Services.Order
    {
        public class Air : IShipping
        {
            public double GetCost(OrderService order)
            {
                if (order.GetTotal() < 100)
                {
                    return 0;
                }

                return Math.Max(20, order.GetTotalWeight()*3);
            }

            public DateTimeOffset GetDate()
            {
                return DateTime.Now.AddDays(7);
            }
        }
    }

-Ground

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

-Sea

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

and at the end we show you main Oreder service.

    using EShop.Models.Shop;

    namespace EShop.Services.Order
    {
        public class OrderService
        {        
            private IList<Product> lineItems;
            private IShipping shipping;

            public OrderService(IList<Product> products) =>
                lineItems = products;

            public int GetTotal() => lineItems.Count;
            public double GetTotalWeight() => lineItems.Sum(x => x.Weight);
            public void SetShippingType(IShipping shippingType) => 
                shipping = shippingType;
            
            public double GetShippingCost()
            {
                return shipping.GetCost(this);
            }
            public DateTimeOffset GetShippingDate() => DateTime.Now;
        }
    }

Main part is switching the shippings using IShipping interface because every shipping type classes inherit from it. We can implement **'O'** in the __Solid__.

Setting Shipping type : 

    public void SetShippingType(IShipping shippingType) =>      
        shipping = shippingType;

---

## Result.

![result](https://github.com/Hamroliyev/EShop/blob/main/Assets/result.gif)



