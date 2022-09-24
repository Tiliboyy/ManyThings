using GameCore;
using HarmonyLib;
using MapEditorReborn.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Log = Exiled.API.Features.Log;

namespace ManyTweaksPatches
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
