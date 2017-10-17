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

        private string m_rootPathToIconImages;

        public string RootPathToIconImages
        {
            get
            {
                if (m_rootPathToIconImages == null)
                    m_rootPathToIconImages = FactorioSettingsDal.LoadSetting(FactorioSettingsDal.XmlRootIconImagePath);
                return m_rootPathToIconImages;
            }
            set
            {
                m_rootPathToIconImages = value;
            }
        }

        public FactorioSettings()
        {

        }

    }
}
