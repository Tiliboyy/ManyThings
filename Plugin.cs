using Exiled.API.Features;
using Exiled.CustomItems;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using HarmonyLib;
using ManyThings;
using System;
using Player = Exiled.Events.Handlers.Player;

public class Plugin : Plugin<Config, Translation>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyThings";
    public override Version Version => new Version(1, 0, 0);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);

    public EventHandlers EventHandler;
    public static Plugin Instance;


    public override void OnEnabled()
    {
        try
        {
            Plugin.Instance = this;

            new Harmony("ManyThings.patches").PatchAll();

            EventHandler = new EventHandlers();

            Exiled.Events.Handlers.Server.RoundEnded += EventHandler.OnRoundEnd;
            
            Player.Verified += EventHandler.VerifiedPlayer;

            Exiled.Events.Handlers.Server.WaitingForPlayers += this.EventHandler.WaitingForPlayers;

            Exiled.Events.Handlers.Server.RoundStarted += this.EventHandler.OnRoundStart;

            Player.DroppingAmmo += this.EventHandler.OnDroppingAmmo;

            Player.Died += EventHandler.OnDied;

            Player.Spawned += EventHandler.OnSpawned;

            Player.DroppingItem += EventHandler.OnDrop;

            Player.ThrowingItem += EventHandler.OnThrow;

            Exiled.Events.Handlers.Warhead.Starting += EventHandler.OnNukeEnabled;

            Log.Info($"ManyThings v{Version} by Tiliboyy has been loaded!");
        }
        catch (Exception error)
        {
            
                Log.Error($"Error while loading plugin: {error}");
        }
    }


    public override void OnDisabled()
    {
        new Harmony("ManyThings.patches").UnpatchAll();
        
        Plugin.Instance = null;

        EventHandler = null;

        Exiled.Events.Handlers.Server.RoundEnded -= EventHandler.OnRoundEnd;


        Exiled.Events.Handlers.Player.Verified -= EventHandler.VerifiedPlayer;

        Exiled.Events.Handlers.Server.WaitingForPlayers -= this.EventHandler.WaitingForPlayers;

        Exiled.Events.Handlers.Server.RoundStarted -= this.EventHandler.OnRoundStart;

        Player.DroppingAmmo -= this.EventHandler.OnDroppingAmmo;

        Player.Died -= EventHandler.OnDied;

        Player.Spawned -= EventHandler.OnSpawned;
        
        Player.DroppingItem -= EventHandler.OnDrop;

        Player.ThrowingItem -= EventHandler.OnThrow;

        Exiled.Events.Handlers.Warhead.Starting -= EventHandler.OnNukeEnabled;


    }
}
