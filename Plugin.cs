using Exiled.API.Features;
using HarmonyLib;
using ManyThings;
using ManyThings.Lobby;
using System;
using Player = Exiled.Events.Handlers.Player;

public class Plugin : Plugin<Config, Translation>
{
    public override string Author => "Tiliboyy";
    public override string Prefix => "ManyThings";
    public override Version Version => new Version(1, 2, 0);
    public override Version RequiredExiledVersion => new Version(5, 0, 0, 0);
    public LobbyEventHandlers LobbyEventHandlers;
    public EventHandlers EventHandler;
    public static Plugin Instance;


    public override void OnEnabled()
    {
        Plugin.Instance = this;
        new Harmony("ManyThings.patches").PatchAll();
        EventHandler = new EventHandlers();
        LobbyEventHandlers = new LobbyEventHandlers();
        Exiled.Events.Handlers.Server.RoundEnded += EventHandler.OnRoundEnd;
        Player.Verified += LobbyEventHandlers.VerifiedPlayer;
        Exiled.Events.Handlers.Server.WaitingForPlayers += this.LobbyEventHandlers.WaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundStarted += this.LobbyEventHandlers.OnRoundStart;
        Exiled.Events.Handlers.Server.RoundStarted += this.EventHandler.OnRoundStart;
        Player.SpawningRagdoll += EventHandler.RagdollSpawning;
        Player.DroppingAmmo += this.EventHandler.OnDroppingAmmo;
        Player.Died += LobbyEventHandlers.OnDied;
        Player.Spawned += LobbyEventHandlers.OnSpawned;
        Player.Spawned += EventHandler.OnSpawned;
        Player.DroppingItem += LobbyEventHandlers.OnDrop;
        Player.ThrowingItem += LobbyEventHandlers.OnThrow;
        Log.Info($"ManyThings v{Version} by Tiliboyy has been loaded!");
    }


    public override void OnDisabled()
    {
        new Harmony("ManyThings.patches").UnpatchAll();
        Plugin.Instance = null;
        EventHandler = null;
        LobbyEventHandlers = null;
        Exiled.Events.Handlers.Server.RoundEnded -= EventHandler.OnRoundEnd;
        Exiled.Events.Handlers.Player.Verified -= LobbyEventHandlers.VerifiedPlayer;
        Exiled.Events.Handlers.Server.WaitingForPlayers -= this.LobbyEventHandlers.WaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundStarted -= this.LobbyEventHandlers.OnRoundStart;
        Player.DroppingAmmo -= this.EventHandler.OnDroppingAmmo;
        Player.Died -= LobbyEventHandlers.OnDied;
        Player.Spawned -= LobbyEventHandlers.OnSpawned;
        Player.Spawned -= EventHandler.OnSpawned;
        Player.DroppingItem -= LobbyEventHandlers.OnDrop;
        Player.ThrowingItem -= LobbyEventHandlers.OnThrow;
        Exiled.Events.Handlers.Server.RoundStarted -= this.EventHandler.OnRoundStart;
    }
}
