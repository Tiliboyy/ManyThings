using CommandSystem;
using Exiled.API.Extensions;
using Exiled.Permissions.Extensions;
using System;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SpeedDeSynced : ICommand
    {
        public string Command { get; } = "SpeedDeSynced";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Makes you very fast but can Desync you";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            float.TryParse(arguments.Array[2], out float speednumother);


            if (sender.CheckPermission("ManyThings.Speed"))
            {
                if (arguments.Count != 2)
                {
                    response = $"<color=yellow>Usage: {Command} <PlayerID> <Speed> </color>";
                    return false;

                }
                Player pl = Player.Get(arguments.At(0));
                if (pl == null)
                {
                    response = $"Player not found: {arguments.At(0)}";
                    return false;
                }
                else if (pl.Role == RoleType.Spectator || pl.Role == RoleType.None)
                {
                    response = $"Player {pl.Nickname} is not a valid class to set speed";
                    return false;
                }
                else if (speednumother == 0)
                {
                    pl.ChangeWalkingSpeed(1.05f, false);

                    pl.ChangeRunningSpeed(1.25f, false);
                    response = $"Resetted {pl.Nickname}'s speed";
                    return true;
                }
                else
                {
                    pl.ChangeWalkingSpeed(speednumother, false);
                    pl.ChangeRunningSpeed(speednumother, false);
                    response = "Set speed of " + pl.Nickname + " to " + speednumother + "!";
                    return true;

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
