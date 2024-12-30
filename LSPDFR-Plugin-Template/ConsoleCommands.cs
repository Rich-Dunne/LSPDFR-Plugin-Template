using LSPD_First_Response.Mod.API;
using Rage;
using Rage.Attributes;
using Rage.ConsoleCommands.AutoCompleters;

namespace LSPDFR_Plugin_Template
{
    internal static class ConsoleCommands
    {
        /// <summary>
        /// This is an example command that the player can execute from the in-game console by typing "End Callout"
        /// </summary>
        /// <param name="endCallout"></param>
        [ConsoleCommand(Name = "End Callout", Description = "An example of a working command for the LSPDFR Plugin Template")]
        internal static void Command_EndCallout([ConsoleCommandParameter(AutoCompleterType = typeof(ConsoleCommandAutoCompleterBoolean))] bool endCallout = true)
        {
            if (!endCallout)
            {
                return;
            }

            if(!Functions.IsCalloutRunning())
            {
                Game.LogTrivial($"There is no callout currently running.");
                return;
            }

            Functions.StopCurrentCallout();
        }
    }
}
