using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Factorio.DAL
{
    public static class FactorioSettingsDAL
    {
        // Get path to Settings in AppData
        private static string pathToSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"FactorioCalculator\Settings.xml");

        private static string pathToIconImages;

        public static string PathToIconImages
        {
            get
            {
                if (pathToIconImages == null)
                    pathToIconImages = loadSettings("PathToIconImages");

                return pathToIconImages;
            }
            set { pathToIconImages = value; }
        }

        private static string loadSettings(string settingName)
        {
            if (File.Exists(pathToSettings) == false)
                FactorioXmlHelper.CreateXml(pathToSettings);

            XDocument settings = XDocument.Load(pathToSettings);

            return settings.Element(settingName).FirstAttribute.ToString();
        }
    }
}
