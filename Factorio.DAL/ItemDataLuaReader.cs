using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.DAL
{
    public class ItemDataLuaReader
    {

        public string Path { get; set; }

        private StreamReader m_streamReader;

        private readonly char m_startOfObject = '{';
        private readonly char m_endOfObject = '}';

        public struct ItemData
        {
            public string type;
            public string name;
            public string icon;
            public string subgroup;
        }

        public ItemDataLuaReader(string path)
        {
            Path = path;

            m_streamReader = new StreamReader(Path);
        }

        public ItemData? ReadNextItemData()
        {
            readToNextItem();

            return null;
        }

        public List<ItemData> ReadAllItemData()
        {
            throw new NotImplementedException();
        }

        private void readToNextItem()
        {
           
            while (!m_streamReader.EndOfStream)
            {
                if(m_streamReader.Read() == m_startOfObject)
                {
                    if (m_streamReader.Peek() != m_startOfObject)
                        return;
                }
            }
        }

    }
}
