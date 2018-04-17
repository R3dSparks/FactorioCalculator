using Factorio.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioCalculator.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            ItemDataLuaReader idlr = new ItemDataLuaReader(@"C:\Users\Sebastian\Desktop\Factorio_0.16.32\data\base\prototypes\item\item.lua");
            idlr.ReadNextItemData();
        }
    }
}
