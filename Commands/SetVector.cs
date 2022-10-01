using CommandSystem;
using Exiled.Permissions.Extensions;
using ManyThings.Lobby;
using System;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SetVector : ICommand
    {
        public string Command { get; } = "SetVector";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Changes your Vector from Spawnpoint used for getting Positions";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            float x;
            float y;
            float z;
            if (sender.CheckPermission("ManyThings.Tools"))
            {
                if (arguments.Count < 3)
                {
                    response = $"<color=yellow>Usage: {Command} <X> <Y> <Z> </color>";
                    return false;
                }
                else
                {
                    float.TryParse(arguments.Array[1], out x);
                    float.TryParse(arguments.Array[2], out y);
                    float.TryParse(arguments.Array[3], out z);

                }

                player.Position = LobbyEventHandlers.SpawnPoint + new Vector3(x, y, z);
                response = "Teleported!";

                return true;

            }
            response = "You do not have the required Permissions for that!";
            return false;
        }
    }
}
