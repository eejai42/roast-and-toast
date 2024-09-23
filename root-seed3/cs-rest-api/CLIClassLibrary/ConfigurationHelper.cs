using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ASPNet_REST_API;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;

namespace CLIClassLibrary
{

    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        static ConfigurationHelper()
        {
            ConfigurationHelper.InitializeAppSettings("appsettings.json");
        }

        public static void InitializeAppSettings(string appSettingsFile, bool failIfNotExists = false)
        {
            var settingsFileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appSettingsFile));
            if (!settingsFileInfo.Exists)
            {
                if (failIfNotExists) throw new FileNotFoundException($"File: {settingsFileInfo.FullName} does not exist.");
                return;
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(settingsFileInfo.Directory.FullName)
                .AddJsonFile(settingsFileInfo.Name, optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetValue(string key)
        {
            return _configuration[key];
        }

        // If you use strongly-typed settings:
        public static T Get<T>(Action<BinderOptions> options)
        {
            return _configuration.Get<T>(options);
        }

        public static IConfigurationSection GetSection(string sectionName)
        {
            return _configuration.GetSection(sectionName);
        }

        public static Auth0Settings GetAuth0Settings()
        {
            var settings = new Auth0Settings();

            var auth0ConfigSection = GetSection("auth0");
            auth0ConfigSection.Bind(settings);

            return settings;
        }
    }

}
