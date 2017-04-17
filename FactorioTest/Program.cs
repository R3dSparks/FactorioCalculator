using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using static System.Console;
using Factorio.Entities;

namespace FactorioTest
{


    class Program
    {
        private static string path = "ItemList.xml";


        /// <summary>
        /// main programm
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            IFactorioLogic logic = new FactorioLogic();
            bool isRunning = true;

            logic.ReadFile(path);

            while (isRunning)
            {
                MainMenu();

                switch (ReadLine())
                {
                    case "1":
                        AddItem(logic);
                        break;
                    case "2":
                        AddRecipe(logic);
                        break;
                    case "3":
                        CreateProduction(logic);
                        break;
                    case "4":
                        ViewItems(logic);
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }

        }



        #region Menus


        /// <summary>
        /// display the menu
        /// </summary>
        private static void MainMenu()
        {
            Clear();
            WriteLine("Factorio calculator v0.1");
            WriteLine();
            WriteLine("1) Add new item");
            WriteLine("2) Add recipe to item");
            WriteLine("3) Create production");
            WriteLine("4) View items");
            WriteLine("5) Exit");
            Write(">");
        }


        /// <summary>
        /// show the names of all items and ask if you want to 
        /// </summary>
        private static void ViewItems(IFactorioLogic logic)
        {
            Clear();
            WriteLine("View items");
            WriteLine();

            foreach (var i in logic.Items)
            {
                WriteLine(i.Name);
            }

            WriteLine();

            Write("View item details?(y/n)");


            if (ReadKey(true).KeyChar == 'y')
            {
                showDetails(logic);
            }
        }


        /// <summary>
        /// show all items in detail
        /// </summary>
        /// <param name="logic"></param>
        private static void showDetails(IFactorioLogic logic)
        {
            WriteLine();
            WriteLine();
            Write("Item: ");

            string itemName = ReadLine();

            FactorioItem item = logic.Items.Find(x => x.Name == itemName);

            WriteLine();
            WriteLine($"Name: {item.Name}");
            WriteLine($"Productivity: {item.Productivity}");
            WriteLine();
            if (item.Recipe != null)
            {
                WriteLine("Recipe:");

                foreach (var craft in item.Recipe)
                {
                    WriteLine($"\t{craft.Value}x {craft.Key.Name}");
                }
            }
            else
            {
                WriteLine("This item has no recipe");
            }

            WriteLine();
            Write("Press any key...");
            ReadKey();
        }


        /// <summary>
        /// read a item name and output the production of it
        /// </summary>
        /// <param name="logic"></param>
        private static void CreateProduction(IFactorioLogic logic)
        {
            Clear();
            WriteLine("View production chain");
            WriteLine();
            Write("Item: ");

            string itemName = ReadLine();

            if (logic.Items.Find(x => x.Name == itemName) == null)
            {
                WriteLine($"Error: Item {itemName} does not exist! Press any key to continue...");
                ReadKey();
                return;
            }

            Assembly assembly = new Assembly(logic.Items.Find(x => x.Name == itemName));

            WriteLine("Get production chain for Assembly machine quantity or items per second?(1/2)...");

            char key = ReadKey(true).KeyChar;

            WriteLine();

            if(key == '1')
            {
                Write("Quantity: ");

                int quantity = Convert.ToInt32(ReadLine());

                WriteLine(assembly.GetProductionPerAssembly(quantity));

                WriteLine();

                WriteLine(assembly.GetItemSummary(quantity));
            }
            else if(key == '2')
            {
                Write("Items per second: ");

                double itemsPerSecond = Convert.ToDouble(ReadLine());

                WriteLine(assembly.GetProductionPerSecond(itemsPerSecond));

                WriteLine();

                WriteLine(assembly.GetItemSummary(itemsPerSecond / assembly.AssemblyItem.Productivity));
            }



            ReadKey();
        }


        /// <summary>
        /// add a recipe to an item.
        /// first it selects the item where the can edit the recipe.
        /// then the user can add a item to the recipe by entering the item name and the amount which is needed.
        /// </summary>
        /// <param name="logic"></param>
        private static void AddRecipe(IFactorioLogic logic)
        {
            Clear();
            WriteLine("Add recipe to item");
            WriteLine();

            foreach (var i in logic.Items)
            {
                WriteLine(i.Name);
            }

            WriteLine();
            Write("Item: ");

            string recipeItem = ReadLine();
            FactorioItem item = logic.Items.Find(x => x.Name == recipeItem);

            addItemToRecipe(logic, item);
            logic.WriteFile(path);
        }


        /// <summary>
        /// continue adding items to the recipe until the user enters not the character 'y' at the end.
        /// </summary>
        private static void addItemToRecipe(IFactorioLogic logic, FactorioItem item)
        {
            do
            {
                string itemName;
                int quantity;

                Clear();
                WriteLine($"Add recipe to {item.Name}");
                WriteLine();

                foreach (var i in logic.Items)
                {
                    if (i.Name != item.Name)
                        WriteLine(i.Name);
                }

                WriteLine();
                Write("Item name: ");
                itemName = ReadLine();

                if(logic.Items.Find(x => x.Name == itemName) == null)
                {
                    WriteLine($"Error: Item {itemName} does not exist! Press any key to continue...");
                    ReadKey();
                    return;
                }

                Write("Item quantity: ");
                quantity = Convert.ToInt32(ReadLine());

                item.AddRecipeItem(logic.Items.Find(x => x.Name == itemName), quantity);

                WriteLine();
                Write("Do you want to add another item to the recipe?(y/n)");

            } while (ReadKey(true).KeyChar == 'y');
        }


        /// <summary>
        /// Add a new item into the list
        /// </summary>
        /// <param name="logic"></param>
        public static void AddItem(IFactorioLogic logic)
        {
            string name;
            int quantity;
            double crafttime;

            Clear();
            WriteLine("Add new item");
            WriteLine();
            Write("Name: ");
            name = ReadLine();

            if (logic.Items.Find(x => x.Name == name) != null)
            {
                WriteLine("Error: Item already exists!");
                ReadKey();
                return;
            }

            Write("Quantity: ");
            quantity = Convert.ToInt32(ReadLine());
            Write("Crafttime: ");
            crafttime = Convert.ToDouble(ReadLine());

            logic.Items.Add(new FactorioItem(name, quantity, crafttime));
            logic.WriteFile(path);

        }


        #endregion
    }
}
