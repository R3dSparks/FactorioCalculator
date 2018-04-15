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


        public struct ItemData
        {
            string type;
            string name;
            string icon;
            string subgroup;
        }

        public ItemDataLuaReader(string path)
        {
            Path = path;

            m_streamReader = new StreamReader(Path);
        }

        public ItemData? ReadNextItemData()
        {
            throw new NotImplementedException();

            if (m_streamReader.EndOfStream)
                return null;

            readToNextItem();



        }

        public List<ItemData> ReadAllItemData()
        {
            throw new NotImplementedException();
        }

        private void readToNextItem()
        {
                while (!m_streamReader.EndOfStream && m_streamReader.Read() != '{' && m_streamReader.Peek() != '{') ;
        }

    }
}
