using System;
using System.IO;
using System.Linq;
using Rage;
using System.Reflection;

namespace LSPDFR_Plugin_Template
{
    internal static class Settings
    {
        private const string INI_FILENAME = "TemplateSettings.ini";
        private static string _iniLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Plugins\LSPDFR", INI_FILENAME);

        // Features
        internal static bool EnableExampleFeature { get; private set; } = false;

        internal static void Prepare()
        {
            if (!File.Exists(_iniLocation))
            {
                Game.LogTrivial($"INI file missing, creating file...");
                CreateINIFile();
            }

            LoadSettings();
        }

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
                        Game.LogTrivial($"File copied successfully to {_iniLocation}");
                    }
                }

            }
            catch (IOException ex)
            {
                Game.LogTrivial($"An error occurred: {ex.Message}");
            }
        }

        private static void LoadSettings()
        {
            Game.LogTrivial($"Loading LSPDFR-Plugin-Template settings...");
            InitializationFile ini = new InitializationFile(_iniLocation);
            ini.Create();

            // Features
            EnableExampleFeature = ini.ReadBoolean("Features", "EnableExampleFeature", false);
            Game.LogTrivial($"EnableExampleFeature is {EnableExampleFeature}");
        }
    }
}
