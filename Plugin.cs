using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using HarmonyLib;
using ManyTweaks;
using System;
using Player = Exiled.Events.Handlers.Player;

public class Plugin : Plugin<Config, Translation>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyTweaks";
    public override Version Version => new Version(3, 0, 0);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);

    public EventHandlers EventHandler;
    public static Plugin Instance;


    public override void OnEnabled()
    {
        try
        {
            new Harmony("ManyTweaks.patches").PatchAll();
            Plugin.Instance = this;


            EventHandler = new EventHandlers();

            Player.Hurting += this.EventHandler.OnHurting;

            Player.Verified += EventHandler.VerifiedPlayer;

            Exiled.Events.Handlers.Server.WaitingForPlayers += this.EventHandler.WaitingForPlayers;

            Exiled.Events.Handlers.Server.RoundStarted += this.EventHandler.OnRoundStart;

            Player.DroppingAmmo += this.EventHandler.OnDroppingAmmo;

            Player.Died += EventHandler.OnDied;

            Player.Spawned += EventHandler.OnSpawned;

            Player.DroppingItem += EventHandler.OnDrop;

            Player.ThrowingItem += EventHandler.OnThrow;

            Exiled.Events.Handlers.Warhead.Starting += EventHandler.OnNukeEnabled;

            //CustomItem.RegisterItems();

            Log.Info($"ManyTweaks v{Version} by Tiliboyy has been loaded!");
        }
        catch (Exception e)
        {
            
                Log.Error($"Error while loading plugin: {e}");
        }
    }


    public override void OnDisabled()
    {
        new Harmony("ManyTweaks.patches").UnpatchAll();
        Plugin.Instance = null;

        EventHandler = null;
        

        Player.Hurting -= this.EventHandler.OnHurting;

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
