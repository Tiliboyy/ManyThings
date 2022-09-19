using System;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using Exiled.Permissions.Extensions;
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
            int speednum;

            if (sender.CheckPermission("ManyTweaks.Speed"))
            {



                if (arguments.Count < 1)
                {
                    response = $"<color=yellow>Usage: {Command} <Speed> </color>";
                    return false;
                }
                else
                {
                    int.TryParse(arguments.Array[1], out speednum);

                }

                if (!player.GetEffectActive<MovementBoost>())
                {
                    player.EnableEffect<MovementBoost>();
                    player.ChangeEffectIntensity<MovementBoost>((byte)speednum);
                    response = "Added Speed!";
                    return true;
                }
                else
                {
                    player.DisableEffect<MovementBoost>();
                    response =  " Removed Speed!";
                    return true;
                }

            }
            else
            {
                response = "You dont have the Perms for that command!";
                return true;

            }
        }
    }
}