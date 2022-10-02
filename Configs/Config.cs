using Discord;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class Config : IConfig
{

    [Description("Enables the Plugin")]
    public bool IsEnabled { get; set; } = true;

    [Description("Enables Debug mode")]
    public bool IsDebug { get; set; } = false;

    [Description("Merges Ammo of the same type")]
    public bool AntiLag { get; set; } = true;

    [Description("Might fix the sprint bug (not tested)")]
    public bool AntiSprintBug { get; set; } = false;

    [Description("Auto Frienly Fire on Round End")]
    public bool AutoFFToggle { get; set; } = false;

    [Description("Adds a countdown for the Alpha Warhead")]
    public bool NukeCountdown { get; set; } = false;
    [Description("Determines the position of the Hint on the users screen (32 = Top, 0 = Middle, -15 = Below)")]
    public int NukeHintVertPos { get; set; } = 32;

    [Description("Lobby Stuff")]
    public bool DisplayWaitMessage { get; set; } = true;

    [Description("Enables global voice chat in the lobby")]
    public bool GlobalVoiceChat { get; set; } = false;
    [Description("The delay it takes to spawn the player in the lobby")]
    public float SpawnDelay { get; set; } = 0.5f;
    
    [Description("Determines the position of the Hint on the users screen (32 = Top, 0 = Middle, -15 = Below)")]
    public int HintVertPos { get; set; } = 25;

    public bool UseHints { get; set; } = true;


    [Description("Allows players to drop items in the lobby")]

    public bool AllowDroppingItem { get; set; } = false;

    [Description("A List of Schematics that are randomly chosen from")]

    public List<string> LobbySchematics { get; private set; } = new List<string>()
        {
            "Lobby Blau",
            "Lobby Discord",
            "Lobby Lila",
            "Lobby PornHub",
            "Lobby Red",
            "Lobby Spotify",
        };

    [Description("The items a player gets in the Lobby")]

    public List<ItemType> LobbyItems { get; private set; } = new List<ItemType>()
        {
            ItemType.Coin,
            ItemType.Flashlight,
        };
    [Description("The Roles that players can spawn as in the lobby")]

    public List<RoleType> RolesToChoose { get; private set; } = new List<RoleType>()
        {
            RoleType.ChaosMarauder,
            RoleType.Scientist,
            RoleType.NtfCaptain,
            RoleType.Tutorial,
            RoleType.FacilityGuard,
            RoleType.ClassD,
    };
    [Description("List of ammo given to a player while in lobby:")]
    public Dictionary<AmmoType, ushort> Ammo { get; private set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato556, 0 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 0 },
            { AmmoType.Ammo12Gauge, 0 },
            { AmmoType.Ammo44Cal, 0 },
        };
    [Description("Coordinates of where the lobby spawns")]

    public SerializedVector3.SerializedVector3 SpawnPoint { get; set; } = new Vector3(240.1f, 1500, 95.8f);
    [Description("The Rotation of the Player when they spawn")]

    public SerializedVector3.SerializedVector3 SpawnRotation { get; set; } = new Vector3(0, 0, 0);

    [Description("Coordinates of where the spawners are from the spawnpoint of the lobby (use .getvector to get coordinates)")]

    public SerializedVector3.SerializedVector3 ClassDSpawner { get; set; } = new Vector3(-8.39999962f, 0, 5.0999999f);

    public SerializedVector3.SerializedVector3 GuardSpawner { get; set; } = new Vector3(4.9000001f, 0, 15.1999998f);

    public SerializedVector3.SerializedVector3 ScientistSpawner { get; set; } = new Vector3(-5.0999999f, 0, 15f);

    public SerializedVector3.SerializedVector3 ScpSpawner { get; set; } = new Vector3(8.39999962f, 0, 5f);
}


