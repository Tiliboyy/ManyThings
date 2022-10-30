using GameCore;
using HarmonyLib;
using PlayableScps;
using System.Collections.Generic;
using UnityEngine;
using ManyThings;
using Exiled.API.Features;
using Log = Exiled.API.Features.Log;

namespace ManyThings
{
    [HarmonyPatch(typeof(RoundStart), nameof(RoundStart.NetworkTimer), MethodType.Setter)]
    internal static class NetworkTimerPatch
    {
        private static bool Prefix(RoundStart __instance, ref short value)
        {
            if (Round.IsStarted && (GameCore.RoundStart.singleton.NetworkTimer > 1 || GameCore.RoundStart.singleton.NetworkTimer == -2))
            {
                return true;
            }
            if (Plugin.Instance.Config.GlobalVoiceChat)
            {
                return true;
            }
            __instance.Timer = value;
            return false;
        }
    }
}
