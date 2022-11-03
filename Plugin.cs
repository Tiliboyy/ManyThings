using Exiled.API.Features;
using HarmonyLib;
using ManyThings;
using System;
using Player = Exiled.Events.Handlers.Player;
using MapEvent = Exiled.Events.Handlers.Map;
using Exiled.CustomItems.API.Features;
using MEC;

public class Plugin : Plugin<Config, Translation>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyThings";
    public override Version Version => new Version(2, 1, 0);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);
    public LobbyEventHandlers LobbyEventHandlers;
    public EventHandlers EventHandler;
    public static Plugin Instance;

    public override void OnEnabled()
    {
        try
        {
            Plugin.Instance = this;
            new Harmony("ManyThings.patches").PatchAll();
            EventHandler = new EventHandlers();
            LobbyEventHandlers = new LobbyEventHandlers();
            Player.Verified += LobbyEventHandlers.VerifiedPlayer;
            Exiled.Events.Handlers.Server.WaitingForPlayers += this.LobbyEventHandlers.WaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted += this.LobbyEventHandlers.OnRoundStart;
            Exiled.Events.Handlers.Server.RoundStarted += this.EventHandler.OnRoundStart;
            Player.SpawningRagdoll += EventHandler.RagdollSpawning;
            Player.DroppingAmmo += this.EventHandler.OnDroppingAmmo;
            Player.Died += LobbyEventHandlers.OnDied;
            Player.Spawned += LobbyEventHandlers.OnSpawned;
            Player.DroppingItem += LobbyEventHandlers.OnDrop;
            Player.ThrowingItem += LobbyEventHandlers.OnThrow;
            MapEvent.PlacingBlood += LobbyEventHandlers.OnPlacingBlood;
            Player.SpawningRagdoll += LobbyEventHandlers.RagdollSpawning;
            Player.Dying += LobbyEventHandlers.OnDying;

            Log.Info($"ManyThings v{Version} by Tiliboyy has been loaded!");
        }
        catch (Exception e)
        {
            Log.Error(e);
        }

    }


    public override void OnDisabled()
    {
        new Harmony("ManyThings.patches").UnpatchAll();
        Plugin.Instance = null;
        EventHandler = null;
        LobbyEventHandlers = null;
        Player.Verified -= LobbyEventHandlers.VerifiedPlayer;
        Exiled.Events.Handlers.Server.WaitingForPlayers -= this.LobbyEventHandlers.WaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundStarted -= this.LobbyEventHandlers.OnRoundStart;
        Exiled.Events.Handlers.Server.RoundStarted -= this.EventHandler.OnRoundStart;
        Player.SpawningRagdoll -= EventHandler.RagdollSpawning;
        Player.DroppingAmmo -= this.EventHandler.OnDroppingAmmo;
        Player.Died -= LobbyEventHandlers.OnDied;
        Player.Spawned -= LobbyEventHandlers.OnSpawned;
        Player.DroppingItem -= LobbyEventHandlers.OnDrop;
        Player.ThrowingItem -= LobbyEventHandlers.OnThrow;
        MapEvent.PlacingBlood -= LobbyEventHandlers.OnPlacingBlood;
        
    }
}
