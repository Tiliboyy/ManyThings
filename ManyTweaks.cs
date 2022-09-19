using System;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using Player = Exiled.Events.Handlers.Player;

public class ManyTweaks : Plugin<Config>
{

    public override void OnEnabled()
    {
        Log.Info("ManyTweaks v1.1.0 by Tiliboyy has been enabled!");

        ManyTweaks.Singleton = this;

        this.EventHandler = new EventHandlers();

        

        Player.Hurting += this.EventHandler.OnHurting;

    }

    public override void OnDisabled()
    {

        ManyTweaks.Singleton = null;

        this.EventHandler = null;
    }

    public override void OnReloaded()
    {
        base.OnReloaded();
    }

    public EventHandlers EventHandler;

    public static ManyTweaks Singleton;
}
