using Factorio.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Factorio.DAL
{
    public class FactorioSettingsDal
    {
        #region XmlMarkers
        public static readonly string XmlMainElement = "Settings";
        public static readonly string XmlRootIconImagePath = "RootIconImagePath";
        #endregion

        private string m_pathToSettings;

        public FactorioSettingsDal(string path)
        {
            m_pathToSettings = path;
        }

        public string LoadSetting(string settingName)
        {
            if (File.Exists(m_pathToSettings) == false)
                FactorioXmlHelper.CreateXml(m_pathToSettings, XmlMainElement);

            XDocument settings = XDocument.Load(m_pathToSettings);

            if(settings.Element(settingName).FirstAttribute == null)
                FactorioXmlHelper.AddAttribute(m_pathToSettings, settingName);


            return settings.Element(settingName).FirstAttribute.ToString();
        }

        public void SaveSetting(string settingName, string value)
        {
            if (File.Exists(m_pathToSettings) == false)
                FactorioXmlHelper.CreateXml(m_pathToSettings, XmlMainElement);

            XDocument settings = XDocument.Load(m_pathToSettings);

            settings.Element(settingName).FirstAttribute.SetValue(value);
        }
    }
}
