using System.Reflection;
using LSPD_First_Response.Mod.API;
using Rage;

namespace LSPDFR_Plugin_Template
{
    public class Main : Plugin
    {
        private static string _assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override void Initialize()
        {
            Game.AddConsoleCommands();
            Settings.Prepare();
            Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;
        }

        private static void OnOnDutyStateChangedHandler(bool OnDuty)
        {
            if (OnDuty)
            {
                Game.LogTrivial($"LSPDFR Plugin Template V{_assemblyVersion} is loaded.");
            }
        }

        public override void Finally()
        {
            Game.LogTrivial($"Plugin been cleaned up.");
        }
    }
}
