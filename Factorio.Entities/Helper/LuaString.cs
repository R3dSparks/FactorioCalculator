using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Helper
{
    /// <summary>
    /// A string formated for lua code
    /// </summary>
    public class LuaString
    {
        private List<string> m_StringList;

        public List<string> StringList
        {
            get { return m_StringList; }
            set { m_StringList = value; }
        }

        public LuaString()
        {

        }



    }
}
