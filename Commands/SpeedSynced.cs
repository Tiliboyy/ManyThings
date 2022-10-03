using CommandSystem;
using CustomPlayerEffects;
using Exiled.Permissions.Extensions;
using System;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SpeedSynced : ICommand
    {
        public string Command { get; } = "SpeedSynced";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Makes you fast without desync";

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
                else
                {
                    pl.EnableEffect(Exiled.API.Enums.EffectType.MovementBoost);
                    pl.ChangeEffectIntensity<MovementBoost>((byte)speednumother);
                    response = "Set speed of" + pl.Nickname + " to " + speednumother + "!";
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
