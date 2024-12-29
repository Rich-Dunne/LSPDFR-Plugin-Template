using LSPD_First_Response.Mod.API;
using Rage.Attributes;
using Rage.ConsoleCommands.AutoCompleters;

namespace LSPDFR_Plugin_Template
{
    internal static class ConsoleCommands
    {
        [ConsoleCommand(Name = "End Callout", Description = "An example of a working command for the LSPDFR Plugin Template")]
        internal static void Command_EndCallout([ConsoleCommandParameter(AutoCompleterType = typeof(ConsoleCommandAutoCompleterBoolean))] bool endCallout = true)
        {
            if (!endCallout)
            {
                return;
            }

            Functions.StopCurrentCallout();
        }
    }
}
