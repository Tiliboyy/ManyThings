using System;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using Player = Exiled.API.Features.Player;

namespace TutorialPlugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Cola : ICommand
    {
        public string Command { get; } = "Cola";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Gives the Player T4 Cola";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (ManyTweaks.Singleton.Config.EnableColaCommand)
            {
                if (!player.GetEffectActive<Scp207>())
                {
                    player.EnableEffect(EffectType.Scp207);
                    player.ChangeEffectIntensity<Scp207>(ManyTweaks.Singleton.Config.ColaIntensity);
                    response = player.Nickname + " Effected!";
                    return true;
                }
                else
                {
                    player.DisableEffect<Scp207>();
                    response = player.Nickname + " Removed Effects!";
                    return true;
                }
            }
            else
            {
                response = "Command is Disabled!";
                return true;

            }
        }
    }
}