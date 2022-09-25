using CommandSystem;
using Exiled.API.Extensions;
using Exiled.Permissions.Extensions;
using System;
using Player = Exiled.API.Features.Player;

namespace TutorialPlugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Speed : ICommand
    {
        public string Command { get; } = "Speed";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Makes you Speed";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            float speednum;
            if (sender.CheckPermission("ManyTweaks.Speed"))
            {
                if (arguments.Count < 1)
                {
                    response = $"<color=yellow>Usage: {Command} <Speed> </color>";
                    return false;
                }
                else
                {
                    float.TryParse(arguments.Array[1], out speednum);
                }


                if (speednum == 0)
                {
                    player.ChangeWalkingSpeed(1.05f, false);

                    player.ChangeRunningSpeed(1.25f, false);
                    response = "Resettet Speed!";
                    return true;

                }
                else
                {
                    player.ChangeWalkingSpeed(speednum, false);

                    player.ChangeRunningSpeed(speednum, false);
                    response = "Added Speed!";
                }
            }
            else
            {
                response = "You do not have the required Permissions for that!";
            }
            return true;


        }
    }
}
