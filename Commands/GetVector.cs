using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using ManyThings.Lobby;
using System;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class GetVector : ICommand
    {
        public string Command { get; } = "GetVector";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Gets your Vector from Spawnpoint used for getting Positions";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (sender.CheckPermission("ManyThings.Tools"))
            {
                var Vector = player.Position - LobbyEventHandlers.SpawnPoint;
                if (Plugin.Instance.Config.IsDebug)
                {
                    Log.Info(Vector);
                }
                response = ($"X: {Vector.x} Y: {Vector.y} Z: {Vector.z}");
                return true;
            }
            response = "You do not have the required Permissions for that!";
            return false;
        }
    }
}
