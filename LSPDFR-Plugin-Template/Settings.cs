﻿using System;
using System.IO;
using System.Linq;
using Rage;
using System.Reflection;

namespace LSPDFR_Plugin_Template
{
    internal static class Settings
    {
        /// <summary>
        /// The name of the INI file used for this plugin's settings.  This value should match the name of the INI file embedded in this project.  Be sure to change the names from the default "TemplateSettings"  Common naming convention is "YourPluginName.ini"
        /// </summary>
        private const string INI_FILENAME = "TemplateSettings.ini";
        
        private static readonly string _iniLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Plugins\LSPDFR", INI_FILENAME);

        #region Settings
        internal static bool EnableExampleFeature { get; set; } = false;
        #endregion

        /// <summary>
        /// Checks if the plugin's INI file exists prior to loading the settings.
        /// </summary>
        internal static void Prepare()
        {
            if (!File.Exists(_iniLocation))
            {
                Game.LogTrivial($"INI file missing, creating file...");
                CreateINIFile();
            }

            Load();
        }

        /// <summary>
        /// Creates an INI file in the \Plugins\LSPDFR directory with the value assigned to <see cref="INI_FILENAME"/>.  The contents of the file are generated from the embedded resource "TemplateSettings.ini".
        /// </summary>
        private static void CreateINIFile()
        {
            Game.LogTrivial($"INI Destination: {_iniLocation}");

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string[] resourceNames = assembly.GetManifestResourceNames();
                string resourceName = resourceNames.FirstOrDefault(name => name.EndsWith(INI_FILENAME));

                if(resourceName is null)
                {
                    Game.LogTrivial($"Embedded resource not found: {INI_FILENAME}");
                    return;
                }

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if(stream is null)
                    {
                        Game.LogTrivial($"Embedded resource stream not found: {resourceName}");
                        return;
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string contents = reader.ReadToEnd();
                        File.WriteAllText(_iniLocation, contents);
                        Game.LogTrivial($"INI created successfully at {_iniLocation}");
                    }
                }

            }
            catch (IOException ex)
            {
                Game.LogTrivial($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Parses the INI file and assigns values to their relevant properties.
        /// </summary>
        private static void Load()
        {
            Game.LogTrivial($"Loading {Assembly.GetExecutingAssembly().GetName().Name} settings...");
            InitializationFile ini = new InitializationFile(_iniLocation);
            ini.Create();

            // Features
            EnableExampleFeature = ini.ReadBoolean("Settings", "EnableExampleFeature", false);
            Game.LogTrivial($"EnableExampleFeature is {EnableExampleFeature}");
        }

        /// <summary>
        /// Saves the current values of the Settings properties to the INI file.  This does not affect the embedded resource file.
        /// </summary>
        internal static void Save()
        {
            if (!File.Exists(_iniLocation))
            {
                Game.LogTrivial($"INI file missing, creating file...");
                CreateINIFile();
            }

            Game.LogTrivial($"Saving {Assembly.GetExecutingAssembly().GetName().Name} settings...");
            InitializationFile ini = new InitializationFile(_iniLocation);
            ini.Create();

            // Features
            ini.Write("Settings", "EnableExampleFeature", EnableExampleFeature);
            Game.LogTrivial($"EnableExampleFeature saved as {EnableExampleFeature}");
        }
    }
}
