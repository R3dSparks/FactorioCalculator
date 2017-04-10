using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace Factorio
{
    public class Production
    {
        private Assembly firstAssembly;
        private List<Item> items;

        public Production(string itemName, List<Item> items)
        {
            this.items = items;
            firstAssembly = new Assembly(items.Find(x => x.Name == itemName));
        }

        public void Print(int quantity)
        {
            WriteLine($"{firstAssembly.AssemblyItem.Name}: {firstAssembly.Quantity * quantity} ({firstAssembly.AssemblyItem.Productivity * quantity}/second)");
            foreach(var subAssembly in firstAssembly.SubAssembly)
            {
                subAssembly.Print(quantity, 1);
            }
        }

    }
}
