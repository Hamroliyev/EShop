//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Brokers.Storages;
using EShop.Models.Auth;
using EShop.Models.Shop;
using EShop.Services.Login;
using EShop.Services.Order;
using EShop.Services.Storage;

namespace EShop
{
    public class Program
    {
        static ILoginService loginService = new LoginService();
        static ICredentialService credentialService = new CredentialService();
        static IProductService productService = new ProductService();
        static IList<IShipping> shippings = new List<IShipping> { new Sea(), new Air(), new Ground() };
        static Credential GetCredential(string username,
                                        string password)
        {
            return new Credential()
            {
                Username = username,
                Password = password
            };
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("------ Welcome to electronik shopping ------");
            Console.Write("Can you input your name : ");
            string userNameInput = Console.ReadLine();
            Console.Write("Can you input your password : ");
            string passwordIput = Console.ReadLine();
            Credential credential = GetCredential(userNameInput, passwordIput);

            if (loginService.CheckUserLogin(credential))
            {
                List<Product> selectedProducts = new List<Product>();

                bool choosingProduct = true;
                do
                {
                    PrintProduct();
                    Console.WriteLine("Select product");
                    string input = Console.ReadLine();
                    int selectedIndex = Convert.ToInt32(input);
                    if (selectedIndex == 0)
                    {
                        choosingProduct = false;
                    }
                    else
                    {
                        Console.Write("Enter weight: ");
                        string inputWeight = Console.ReadLine();
                        double weight = Convert.ToDouble(inputWeight);
                        productService.GetProducts()[selectedIndex - 1].Weight = weight;
                        selectedProducts.Add(productService.GetProducts()[selectedIndex - 1]);
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully added ✔✔");
                        Console.ResetColor();
                    }
                } while (choosingProduct);
                Console.Clear();
                OrderService order = new OrderService(selectedProducts);
                PrintShippingTypes();
                Console.WriteLine("Select shippingType");
                string inputShipping = Console.ReadLine();
                int selectedShipping = Convert.ToInt32(inputShipping);
                order.SetShippingType(shippings[selectedShipping]);
                Console.Clear();
                Console.WriteLine("Shipping successfully added ✔✔");
                PrintOrderDetails(order);
            }
            else
            {
                credentialService.AddCredential(credential);
            }
        }

        static void PrintProduct()
        {
            Console.WriteLine("0) Order create");
            Console.WriteLine("-------Start-of-product-------------");
            int index = 1;
            foreach (var item in productService.GetProducts())
            {
                Console.WriteLine(index++ + ")" + item.Name);

            }
            Console.WriteLine("-----------End-of-product-----------");

        }

        static void PrintShippingTypes()
        {
            Console.WriteLine("-------Start-of-shipping-------------");
            int index = 0;
            foreach (var item in shippings)
            {
                Console.WriteLine(index++ + ")" + item.GetType().Name);

            }
            Console.WriteLine("-----------End-of-shipping-----------");

        }

        static void PrintOrderDetails(OrderService order)
        {
            Console.WriteLine($"Shipping cost: ${order.GetShippingCost()}");
            Console.WriteLine($"Shipping weight: ${order.GetTotalWeight()}");
            Console.WriteLine($"Shipping date: ${order.GetShippingDate()}");
        }
    }
}
