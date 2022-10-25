using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;
using Server = Exiled.API.Features.Server;

public class EventHandlers : Plugin<Config>
{


    public static List<CoroutineHandle> coroutines = new();

    public static System.Random random = new System.Random();

    public void OnRoundStart()
    {
        if (Plugin.Instance.Config.NukeCountdown)
        {
            coroutines.Add(Timing.RunCoroutine(ManyThings.UnityMethods.NukeCountdown()));
        }
        if (Plugin.Instance.Config.AutoFFToggle)
        {
            Server.FriendlyFire = false;
        }
        foreach (Player player in Player.List) 
        {
            player.ClearBroadcasts();
        }
    }

    public void OnRoundEnd(EndingRoundEventArgs ev)
    {

        if (Plugin.Instance.Config.AutoFFToggle)
        {
            Server.FriendlyFire = true;
        }
    }
    public void OnDroppingAmmo(DroppingAmmoEventArgs ev)
    {
        if (Plugin.Instance.Config.AntiLag)
        {
            Timing.RunCoroutine(ManyThings.UnityMethods.DensifyAmmoBoxes(ev));
        }
    }
    public void RagdollSpawning(SpawningRagdollEventArgs ev)
    {
        Timing.RunCoroutine(ManyThings.UnityMethods.DensifyAmmoBoxes(ev));
    }
    public void OnSpawned(SpawnedEventArgs ev)
    {
        if (Plugin.Instance.Config.AntiSprintBug)
        {
            Timing.RunCoroutine(ManyThings.UnityMethods.AntiSprintBug(ev));
        }
    }
    
}

