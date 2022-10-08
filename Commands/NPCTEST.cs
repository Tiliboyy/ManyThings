using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using ManyThings.Lobby;
using System;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class NPCTEST : ICommand
    {
        public string Command { get; } = "NPCTEST";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "NPCTEST";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (sender.CheckPermission("ManyThings.Tools"))
            {
                player.Position = new Vector3(2.3f, 2.3f, 2.3f);
                response = "yes!";
                return true;
            }
            response = "You do not have the required Permissions for that!";
            return false;
        }
    }
}
