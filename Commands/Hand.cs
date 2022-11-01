using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using UnityEngine;
using MEC;
using Player = Exiled.API.Features.Player;
using Server = Exiled.API.Features.Server;

namespace ManyThings
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Hand : ICommand
    {
        public string Command { get; } = "Hand";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Hand";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (sender.CheckPermission("ManyThings.Fun"))
            {
                if (arguments.Count == 2)
                {
                    
                    string id = arguments.Array[1];
                    float duration;

                    if (id == "*")
                    {
                        float.TryParse(arguments.Array[2], out duration);
                        Player player = null;
                        Timing.RunCoroutine(UnityMethods.HandCoroutine(player, duration, true));
                        response = "Handed everyone";
                        return true;
                    }
                    else
                    {
                        Player target = Player.Get(arguments.Array[1]);
                        if (target == null)
                        {
                            response = "Player not found";
                            return false;
                        }
                        else
                        {
                            float.TryParse(arguments.Array[2], out duration);
                            Timing.RunCoroutine(UnityMethods.HandCoroutine(target, duration, false));
                            response = "Handed " + target.DisplayNickname;
                            return true;
                        }

                    }
                }
                else
                {
                    response = "Usage: Hand <ID> <Amount>";
                    return false;
                }
            }
            else
            {
                response = "You do not have the required Permissions for that!";
                return false;
            }
        }
    }
}
