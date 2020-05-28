using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CinemaApp
{
    class CatererFoodOrder
    {
        public List<FoodOrder> Orders;

        public CatererFoodOrder(List<FoodOrder> orders)
        {
            this.Orders = orders;
        }


        public void caterView()
        {
            Console.WriteLine("\nTo see uncompleted orders press 1" + "\nTo see completed orders press 2");
            var choice = Console.ReadKey().Key;

            if (choice == ConsoleKey.D1)
            {
                Console.WriteLine("\nOrders not completed:");
                foreach (var fo in Orders)
                {
                    if (!fo.Made)
                    {
                        Console.WriteLine(fo.OrderId);
                        if (fo.Minute < 10)
                        {
                            Console.WriteLine("Time: " + fo.Hour + ": 0" + fo.Minute);
                        }
                        else
                        {
                            Console.WriteLine("Time: " + fo.Hour + ":" + fo.Minute);
                        }

                        fo.Order.overview();
                    }
                }
            }

            else if (choice == ConsoleKey.D2)
            {
                Console.WriteLine("\nOrders completed:");
                foreach (var fo in Orders)
                {
                    if (fo.Made)
                    {
                        Console.WriteLine(fo.OrderId);
                        fo.displayTime();
                        fo.Order.overview();
                    }
                }
            }
        }

        public void isDone()
        {
            Console.WriteLine("\nEnter Order Id: ");
            var idDelete = Console.ReadLine();
            int id;
            bool sucess = Int32.TryParse(idDelete, out id);

            if ((sucess) & (id <= Orders.Count) & (id >= 0))
            {
                Console.WriteLine();
                foreach (var f in Orders)
                {
                    if (f.OrderId == id)
                    {
                        f.Made = true;
                    }
                }
            }
        }

        public void pickedUp()
        {
            Console.WriteLine("\nEnter Order Id: ");
            var idDelete = Console.ReadLine();
            int id;
            bool sucess = Int32.TryParse(idDelete, out id);

            if ((sucess) & (id <= Orders.Count) & (id >= 0))
            {
                Console.WriteLine();
                FoodOrder toRemove = null;
                foreach (var f in Orders)
                {
                    toRemove = f;
                }
                Orders.Remove(toRemove);
            }
        }

        public static void Caterer()
        {
            string fileName = "allOrders.json";
            string rawJson = File.ReadAllText(fileName);
            List<FoodOrder> all = JsonConvert.DeserializeObject<List<FoodOrder>>(rawJson);
            var allOrders = new CatererFoodOrder(all);
            bool busy = true;

            while (busy)
            {
                Console.WriteLine("\n1.View Orders\n2.Add item to done\n3. Remove a picked up order\npress q to quit....");
                var choice = Console.ReadKey().Key;
                if (choice == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (choice == ConsoleKey.D1)
                {
                    allOrders.caterView();
                }
                else if (choice == ConsoleKey.D2)
                {
                    allOrders.isDone();
                }
                else if (choice == ConsoleKey.D3)
                {
                    allOrders.pickedUp();
                }
            }
            string newJson = JsonConvert.SerializeObject(all);
            File.WriteAllText(fileName, newJson);
        }
    }

}

