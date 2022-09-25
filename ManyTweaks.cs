using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using HarmonyLib;
using System;
using Player = Exiled.Events.Handlers.Player;

public class ManyTweaks : Plugin<Config>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyTweaks";
    public override Version Version => new Version(2, 0, 0);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);

    public EventHandlers EventHandler;
    public static ManyTweaks Singleton;

    public override void OnEnabled()
    {
        new Harmony("ManyTweaks.patches").PatchAll();
        ManyTweaks.Singleton = this;

        this.EventHandler = new EventHandlers();

        Player.Hurting += this.EventHandler.OnHurting;

        Exiled.Events.Handlers.Player.Verified += EventHandler.VerifiedPlayer;

        Exiled.Events.Handlers.Server.WaitingForPlayers += this.EventHandler.WaitingForPlayers;

        Exiled.Events.Handlers.Server.RoundStarted += this.EventHandler.OnRoundStart;

        Player.DroppingAmmo += this.EventHandler.OnDroppingAmmo;

        Player.Died += EventHandler.OnDied;

        Player.Spawned += EventHandler.OnSpawned;

        CustomItem.RegisterItems();
    }

    public override void OnDisabled()
    {
        ManyTweaks.Singleton = null;

        this.EventHandler = null;

        Player.Hurting -= this.EventHandler.OnHurting;

        Exiled.Events.Handlers.Player.Verified -= EventHandler.VerifiedPlayer;

        Exiled.Events.Handlers.Server.WaitingForPlayers -= this.EventHandler.WaitingForPlayers;

        Exiled.Events.Handlers.Server.RoundStarted -= this.EventHandler.OnRoundStart;

        Player.DroppingAmmo -= this.EventHandler.OnDroppingAmmo;

        Player.Died -= EventHandler.OnDied;

        Player.Spawned -= EventHandler.OnSpawned;

    }
}
