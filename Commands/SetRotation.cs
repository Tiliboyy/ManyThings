using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SetRotation : ICommand
    {
        public string Command { get; } = "SetRotation";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Changes your Rotation";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            float x;
            float y;

            if (sender.CheckPermission("ManyThings.Tools"))
            {
                if (arguments.Count < 3)
                {
                    response = $"<color=yellow>Usage: {Command} <X> <Y> </color>";
                    return false;
                }
                else
                {
                    float.TryParse(arguments.Array[1], out x);
                    float.TryParse(arguments.Array[1], out y);
                }

                player.Rotation = new Vector2(x, y);
                response = "Rotated!";

                return true;

            }
            response = "You do not have the required Permissions for that!";
            return false;
        }
    }
}
