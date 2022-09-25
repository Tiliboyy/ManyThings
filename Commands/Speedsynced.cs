using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Extensions;
using Exiled.Permissions.Extensions;
using FMOD;
using System;
using Player = Exiled.API.Features.Player;

namespace TutorialPlugin.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SpeedSynced : ICommand
    {
        public string Command { get; } = "SpeedSynced";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Makes you fast without desync";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            float speednum;
            float playerid;

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
                    float.TryParse(arguments.Array[2], out playerid);
                }


                if (speednum == 0)
                {
                    player.DisableEffect<MovementBoost>();
                    response = "Resettet Speed!";
                    return true;

                }
                else
                {
                    player.EnableEffect(Exiled.API.Enums.EffectType.MovementBoost);
                    player.ChangeEffectIntensity<MovementBoost>((byte)speednum);
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
