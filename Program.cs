//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using EShop.Models.Shop;
using EShop.Services.Order;

namespace EShop
{
    public class Program
    {
        static List<Product> products = new List<Product> {
        new Product { Name = "Banana"},
        new Product { Name = "Ananas"},
        new Product { Name = "Apple"},
        new Product { Name = "Orange"},
    };
        static List<IShipping> shippings = new List<IShipping> { new Sea(), new Air(), new Ground() };

        public static void Main()
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
                    products[selectedIndex - 1].Weight = weight;
                    selectedProducts.Add(products[selectedIndex - 1]);
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

        static void PrintProduct()
        {
            Console.WriteLine("0) Order create");
            Console.WriteLine("-------Start-of-product-------------");
            int index = 1;
            foreach (var item in products)
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
