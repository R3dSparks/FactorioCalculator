using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuaInterface;
using System.IO;
using System.Linq;

namespace Factorio.Original.Dal.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var path = @"E:\Programme\Steam\steamapps\common\Factorio\data\base\prototypes\recipe\recipe.lua";
            var dal = new FactorioDal();

            try
            {
                string file = "";
                var fileContains = File.ReadAllLines(path);
                for (int i = 1; i < fileContains.Length; i++)
                {
                    file += fileContains[i];
                } 
                
                var lua = new Lua();
                var res = lua.DoString(file);
                

                foreach (var item in res)
                {
                    Console.WriteLine(item.ToString());
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
