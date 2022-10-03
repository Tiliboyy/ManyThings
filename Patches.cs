using GameCore;
using HarmonyLib;
using PlayableScps;
using System.Collections.Generic;
using UnityEngine;
using ManyThings;
using Exiled.API.Features;

namespace ManyThings.Patches
{
    //Disables global chat in the lobby
    [HarmonyPatch(typeof(RoundStart), nameof(RoundStart.NetworkTimer), MethodType.Setter)]
    internal static class NetworkTimerPatch
    {
        private static bool Prefix(RoundStart __instance, ref short value)
        {
            if (Round.IsStarted)
            {
                return true;
            }
            __instance.Timer = value;
            return false;
        }
    }
}
