using System;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;

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
            bool flag = !player.GetEffectActive<Scp207>();
            bool result;
            if (flag)
            {
                player.EnableEffect(EffectType.Scp207);
                player.ChangeEffectIntensity<Scp207>(4);
                response = player.Nickname + " Effected!";
                result = true;
            }
            else
            {
                player.DisableEffect<Scp207>();
                response = player.Nickname + " Removed Effects!";
                result = true;
            }
            return result;
        }
    }
}