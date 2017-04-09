using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using static System.Console;

namespace FactorioTest
{
    class Program
    {
        static string path = "ItemList.xml";

        static List<Item> items;

        static void Main(string[] args)
        {

            bool isRunning = true;

            items = XmlIO.ReadItems(path);

            while(isRunning)
            {
                MainMenu();

                switch (ReadLine())
                {
                    case "1":
                        AddItem();
                        break;
                    case "2":
                        AddRecipe();
                        break;
                    case "3":
                        CreateProduction();
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }
        
        }

        static void MainMenu()
        {
            Clear();
            WriteLine("Factorio calculator v0.1");
            WriteLine();
            WriteLine("1) Add new item");
            WriteLine("2) Add recipe to item");
            WriteLine("3) Create production");
            WriteLine("4) Exit");
            Write(">");
        }

        static void CreateProduction()
        {
            Clear();
            WriteLine("Create a new production");
            WriteLine();
            Write("Item: ");

            Production production = new Production(ReadLine(), items);

            Write("Quantity: ");         

            production.Print(Convert.ToInt32(ReadLine()));

            ReadKey();
        }

        static void AddRecipe()
        {
            Clear();
            WriteLine("Add recipe to item");
            WriteLine();

            foreach(var i in items)
            {
                WriteLine(i.Name);
            }

            WriteLine();

            Write("Item: ");

            string recipeItem = ReadLine();

            Item item = items.Find(x => x.Name == recipeItem);

            do
            {
                string itemName;
                int quantity;

                Clear();
                WriteLine($"Add recipe to {item.Name}");
                WriteLine();

                foreach (var i in items)
                {
                    if(i.Name != item.Name)                 
                        WriteLine(i.Name);
                }

                WriteLine();
                Write("Item name: ");
                itemName = ReadLine();
                Write("Item quantity: ");
                quantity = Convert.ToInt32(ReadLine());

                item.AddRecipeItem(items.Find(x => x.Name == itemName), quantity);

                WriteLine();
                Write("Do you want to add another item to the recipe?(y/n): ");

            } while (ReadLine() == "y");

            XmlIO.SaveItems(items, path);
            
        }

        static void AddItem()
        {
            string name;
            int quantity;
            double crafttime;

            Clear();
            WriteLine("Add new item");
            WriteLine();
            Write("Name: ");
            name = ReadLine();
            Write("Quantity: ");
            quantity = Convert.ToInt32(ReadLine());
            Write("Crafttime: ");
            crafttime = Convert.ToDouble(ReadLine());

            items.Add(new Item(name, quantity, crafttime));

            XmlIO.SaveItems(items, path);

        }

    }
}
