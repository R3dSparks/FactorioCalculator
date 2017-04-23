using Factorio.Original.Entities;
using Factorio.Original.Interfaces;
using LuaInterface;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Original.Dal
{
    public class FactorioDal : IFactorioDal
    {
        private Lua m_lua;

        #region Properties


        public Lua ReadLua
        {
            get
            {
                return m_lua;
            }
            private set
            {
                m_lua = value;
            }
        }



        #endregion



        public FactorioDal()
        {
            
        }


        public object[] ReadRecipes(string path)
        {
            this.ReadLua = new Lua();
            return this.ReadLua.DoFile(path);
        }

    }
}
