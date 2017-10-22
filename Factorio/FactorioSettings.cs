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

        private string m_rootPathToIconImages;

        public string RootPathToIconImages
        {
            get
            {
                if (m_rootPathToIconImages == null)
                    m_rootPathToIconImages = m_factorioSettingsDal.LoadSetting(FactorioSettingsDal.XmlRootIconImagePath);
                return m_rootPathToIconImages;
            }
            set
            {
                m_rootPathToIconImages = value;
            }
        }

        public FactorioSettings(string path)
        {
            m_factorioSettingsDal = new FactorioSettingsDal(path);
        }

    }
}
