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
    public static class FactorioSettingsDal
    {
        #region XmlMarkers
        public static readonly string XmlMainElement = "Settings";
        public static readonly string XmlRootIconImagePath = "RootIconImagePath";
        #endregion

        // Get path to Settings in AppData
        private static string pathToSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"FactorioCalculator\Settings.xml");

        public static string LoadSetting(string settingName)
        {
            if (File.Exists(pathToSettings) == false)
                FactorioXmlHelper.CreateXml(pathToSettings, XmlMainElement);

            XDocument settings = XDocument.Load(pathToSettings);

            return settings.Element(settingName).FirstAttribute.ToString();
        }

        public static void SaveSetting(string settingName, string value)
        {
            if (File.Exists(pathToSettings) == false)
                FactorioXmlHelper.CreateXml(pathToSettings, XmlMainElement);

            XDocument settings = XDocument.Load(pathToSettings);

            settings.Element(settingName).FirstAttribute.SetValue(value);
        }
    }
}
