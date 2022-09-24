using System;
using System.Runtime.InteropServices;
using CustomItems.Items;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.Handlers;
using MEC;
using Player = Exiled.Events.Handlers.Player;

public class ManyTweaks : Plugin<Config>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyTweaks";
    public override Version Version => new Version(1, 4, 1);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);

    public EventHandlers EventHandler;
    public static ManyTweaks Singleton;

    public override void OnEnabled()
    {        
        ManyTweaks.Singleton = this;
        
        this.EventHandler = new EventHandlers();
        
        Player.Hurting += this.EventHandler.OnHurting;

        Exiled.Events.Handlers.Player.Verified += EventHandler.VerifiedPlayer;

        Exiled.Events.Handlers.Server.WaitingForPlayers += this.EventHandler.WaitingForPlayers;
        
        Exiled.Events.Handlers.Server.RoundStarted += this.EventHandler.OnRoundStart;

        Player.DroppingAmmo += this.EventHandler.OnDroppingAmmo;

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

    }
}
