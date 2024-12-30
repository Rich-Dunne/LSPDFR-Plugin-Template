using System.Reflection;
using LSPD_First_Response.Mod.API;
using Rage;

namespace LSPDFR_Plugin_Template
{
    public class Main : Plugin
    {
        private static string _assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// This function is called when LSPDFR is loaded.
        /// </summary>
        public override void Initialize()
        {
            Game.AddConsoleCommands();
            Settings.Prepare();
            Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;
        }

        /// <summary>
        /// This function handles logic for when the player goes on and off duty.
        /// </summary>
        /// <param name="OnDuty"></param>
        private static void OnOnDutyStateChangedHandler(bool OnDuty)
        {
            if (OnDuty)
            {
                Game.LogTrivial($"{Assembly.GetExecutingAssembly().GetName()} V{_assemblyVersion} is loaded.");
            }
            else
            {
                Game.LogTrivial($"Player has gone off duty.");
            }
        }

        /// <summary>
        /// This function handles logic for when LSPDFR is unloaded.
        /// </summary>
        public override void Finally()
        {
            Game.LogTrivial($"Plugin been cleaned up.");
        }
    }
}
