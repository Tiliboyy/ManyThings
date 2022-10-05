using GameCore;
using HarmonyLib;
using PlayableScps;
using System.Collections.Generic;
using UnityEngine;
using ManyThings;
using Exiled.API.Features;
using Log = Exiled.API.Features.Log;

namespace ManyThings.Patches
{
    [HarmonyPatch(typeof(RoundStart), nameof(RoundStart.NetworkTimer), MethodType.Setter)]
    internal static class NetworkTimerPatch
    {
        private static bool Prefix(RoundStart __instance, ref short value)
        {
            if (Round.IsStarted)
            {
                return true;
            }
            if (Plugin.Instance.Config.GlobalVoiceChat)
            {
                return true;
            }
            Log.Debug("Executed Patch fake sync var", Plugin.Instance.Config.IsDebug);
            __instance.Timer = value;
            return false;
        }
    }
}
