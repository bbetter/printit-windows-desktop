using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.LocalStorage;

namespace PrintIt_Desktop_4.Model.Core
{
    public static class PreLoader
    {
        public static void PerformPreLoad()
        {
            CreateDirectory();
            CurrentState.Configuration = LoadConfig();
            LoadLocalization(CurrentState.Configuration.Locale);
            DeleteOutdatedFiles();
        }

        private static void CreateDirectory()
        {
            if (!Directory.Exists(Config.GetDirectoryLocation()))
            {
                Directory.CreateDirectory(Config.GetDirectoryLocation());
            }
        }

        private static UserConfiguration LoadConfig()
        {
            var serializer = new XmlSerializer(typeof (UserConfiguration));
            if (File.Exists(Config.GetConfigLocation()))
            {
                if (serializer.CanDeserialize(new XmlTextReader(Config.GetConfigLocation())))
                {
                   return (UserConfiguration) serializer.Deserialize(new StreamReader(Config.GetConfigLocation()));
                }
            }
            return InitDefaultConfig();
        }

        private static UserConfiguration InitDefaultConfig()
        {
            return new UserConfiguration() { Locale = Config.GetDefaultLocale()};
        }

        private static void LoadLocalization(String localeName)
        {
            //todo add values in config
            var resourceName = @"Languages/" + localeName + @".xaml";
            var languageDictionary = Application.LoadComponent(new Uri(resourceName,UriKind.Relative)) as ResourceDictionary;
            CurrentState.LanguageResourceDictionary = languageDictionary;
            App.Current.Resources.MergedDictionaries.Add(languageDictionary);
        }

        private static void DeleteOutdatedFiles()
        {
            //todo: delete them
        }
    }
}
