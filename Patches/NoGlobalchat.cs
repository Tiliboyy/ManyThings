﻿using GameCore;
using HarmonyLib;

namespace ManyThings.Patches
{
    [HarmonyPatch(typeof(RoundStart), nameof(RoundStart.NetworkTimer), MethodType.Setter)]
    internal static class NetworkTimerPatch
    {
        private static bool Prefix(RoundStart __instance, ref short value)
        {

            __instance.Timer = value;
            return false;
        }
    }

}
