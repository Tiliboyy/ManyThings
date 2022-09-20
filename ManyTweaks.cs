using System;
using CustomItems.Items;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.Handlers;
using Player = Exiled.Events.Handlers.Player;

public class ManyTweaks : Plugin<Config>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyTweaks";
    public override Version Version => new Version(1, 3, 1);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);

    public EventHandlers EventHandler;
    public static ManyTweaks Singleton;



    public override void OnEnabled()
    {
        Log.Info("ManyTweaks v1.3.1 by Tiliboyy has been enabled!");
        
        ManyTweaks.Singleton = this;

        this.EventHandler = new EventHandlers();
        
        Player.Hurting += this.EventHandler.OnHurting;
        
        CustomItem.RegisterItems();



    }

    public override void OnDisabled()
    {
        ManyTweaks.Singleton = null;
        
        this.EventHandler = null;
        
        Player.Hurting -= this.EventHandler.OnHurting;
    }

    public override void OnReloaded()
    {
        base.OnReloaded();
    }

}
