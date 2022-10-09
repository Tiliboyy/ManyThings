using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace ManyThings.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class GetRotation : ICommand
    {
        public string Command { get; } = "GetRotation";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Gets your Rotation";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (sender.CheckPermission("ManyThings.Tools"))
            {
                response = player.CameraTransform.rotation.eulerAngles.ToString();
                Log.Debug(player.CameraTransform.rotation.eulerAngles.ToString(), Plugin.Instance.Config.IsDebug);
                return true;
            }
            response = "You do not have the required Permissions for that!";
            return false;
        }
    }
}
