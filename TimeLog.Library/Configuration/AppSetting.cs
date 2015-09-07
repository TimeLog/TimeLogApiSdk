namespace TimeLog.Library.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Caching;

    using Newtonsoft.Json;

    public class AppSetting
    {
        private static Dictionary<string, string> MachineSettings
        {
            get
            {
                var instance =
                    (Dictionary<string, string>)HttpContext.Current.Cache.Get("PersonalConfigurationManagerMachine");
                if (instance == null)
                {
                    instance = new Dictionary<string, string>();

                    var machineAppSettingsPath = string.Format(
                        "{0}appSettings-{1}.json",
                        HttpContext.Current.Server.MapPath("~/"),
                        Environment.MachineName.ToLower());

                    if (File.Exists(machineAppSettingsPath))
                    {
                        instance =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(
                                File.ReadAllText(machineAppSettingsPath));

                        HttpContext.Current.Cache.Add(
                            "PersonalConfigurationManagerMachine",
                            instance,
                            new CacheDependency(machineAppSettingsPath),
                            Cache.NoAbsoluteExpiration,
                            TimeSpan.FromHours(1),
                            CacheItemPriority.Normal,
                            null);
                    }
                    else
                    {
                        File.AppendAllText(machineAppSettingsPath, JsonConvert.SerializeObject(instance));
                    }
                }

                return instance;
            }
        }

        private static Dictionary<string, string> StandardSettings
        {
            get
            {
                var instance =
                    (Dictionary<string, string>)HttpContext.Current.Cache.Get("PersonalConfigurationManagerStandard");
                if (instance == null)
                {
                    instance = new Dictionary<string, string>();

                    var standardAppSettingsPath = string.Format(
                        "{0}appSettings.json",
                        HttpContext.Current.Server.MapPath("~/"));

                    if (File.Exists(standardAppSettingsPath))
                    {
                        instance =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(
                                File.ReadAllText(standardAppSettingsPath));

                        HttpContext.Current.Cache.Add(
                            "PersonalConfigurationManagerStandard",
                            instance,
                            new CacheDependency(standardAppSettingsPath),
                            Cache.NoAbsoluteExpiration,
                            TimeSpan.FromHours(1),
                            CacheItemPriority.Normal,
                            null);
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets a specific application settings entry. First tries from the personal
        /// settings file (appSettings-{machinename}.json), then standard settings file
        /// (appSettings.json) and finally tries to get it from web.config.
        /// </summary>
        /// <param name="key">The String key of the key to locate</param>
        /// <returns>A settings value</returns>
        public string this[string key]
        {
            get
            {
                if (MachineSettings.ContainsKey(key))
                {
                    return MachineSettings[key];
                }

                if (StandardSettings.ContainsKey(key))
                {
                    return StandardSettings[key];
                }

                return ConfigurationManager.AppSettings[key];
            }
        }
    }
}