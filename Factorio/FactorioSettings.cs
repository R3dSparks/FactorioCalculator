using Factorio.DAL;
using Factorio.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio
{
    public class FactorioSettings : IFactorioSettings
    {
        private FactorioSettingsDal m_factorioSettingsDal;

        public string RootPathToIconImages { get; set; }

        public FactorioSettings(string path)
        {
            m_factorioSettingsDal = new FactorioSettingsDal(path);
        }

       

    }
}
