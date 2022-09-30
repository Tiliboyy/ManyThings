using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;

public class EventHandlers : Plugin<Config>
{


    public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();


    public void OnRoundStart()
    {

        if (Config.AutoFFToggle)
        {
            Log.Info("Round Started");
            Server.FriendlyFire = true;
            Log.Info(Server.FriendlyFire);
        }
    }

    public void OnRoundEnd(RoundEndedEventArgs ev)
    {
        if (Config.AutoFFToggle)
        {
            Log.Info("Round Ended");
            foreach (Player player in Player.List)
            {

            }
        }
    }
    public void OnNukeEnabled(StartingEventArgs ev)
    {
        if (Config.NukeCountdown)
        {
            coroutines.Add(Timing.RunCoroutine(ManyThings.UnityMethods.UnityMethods.NukeCountdown()));
        }
    }
    public void OnDroppingAmmo(DroppingAmmoEventArgs ev)
    {
        if (Plugin.Instance.Config.AntiLag)
        {
            Timing.RunCoroutine(ManyThings.UnityMethods.UnityMethods.DensifyAmmoBoxes(ev));
        }
    }
    public void RagdollSpawning(SpawningRagdollEventArgs ev)
    {
        Timing.RunCoroutine(ManyThings.UnityMethods.UnityMethods.DensifyAmmoBoxes(ev));
    }
}

