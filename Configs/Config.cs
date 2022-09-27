using Exiled.API.Features;
using Exiled.API.Interfaces;
using Scp914;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;




public class Config : IConfig
{

    [Description("Enables the Plugin")]
    public bool IsEnabled { get; set; } = true;

    [Description("Enables Debug mode")]
    public bool IsDebug { get; set; } = false;

    public string ConfigFolder { get; set; } = Path.Combine(Paths.Configs, "CustomItems");

    public string ConfigFile { get; set; } = "global.yml";

    [Description("Disables 207 Damage")]
    public bool No207Dmg { get; set; } = false;

    [Description("Merges Ammo of the same type")]
    public bool AntiLag { get; set; } = true;

    [Description("Enables Localchat")]
    public bool GlobalVoiceChat { get; set; } = false;
    [Description("Lobby Stuff")]


    public bool DisplayWaitMessage { get; set; } = true;

    [Description("Determines the position of the Hint on the users screen (32 = Top, 0 = Middle, -15 = Below)")]
    public int HintVertPos { get; set; } = 25;

    public bool UseHints { get; set; } = true;

    [Description("Allows players to drop items in the lobby")]
    
    public bool AllowDroppingItem { get; set; } = false;

    [Description("A List of Schematics that are randomly chosen from")]


    public List<string> LobbySchematicList { get; private set; } = new List<string>()
        {
            "Lobby Blau",
            "Lobby Lila",
            "Lobby Red",

        };
    [Description("The items a player gets in the Lobby")]

    public List<ItemType> LobbyItems { get; private set; } = new List<ItemType>()
        {
        
            ItemType.Coin,
            ItemType.Medkit,

        };
    [Description("The Roles that players can spawn as in the lobby")]

    public List<RoleType> RolesToChoose { get; private set; } = new List<RoleType>()
        {
            RoleType.Tutorial,
            RoleType.ChaosMarauder,
            RoleType.NtfCaptain,
            RoleType.ChaosConscript,
        };
    [Description("Coordinates of where the lobby spawns")]

    public SerializedVector3.SerializedVector3 SpawnPoint { get; set; } = new Vector3(240.1f, 977, 95.8f);
    [Description("The Rotation of the Player when they spawn")]

    public SerializedVector3.SerializedVector3 SpawnRotation { get; set; } = new Vector3(0, 0, 0);

    [Description("Coordinates of where the spawners are from the spawnpoint of the lobby (use .getvector to get coordinates)")]

    public SerializedVector3.SerializedVector3 ClassDSpawner { get; set; } = new Vector3(-8.39999962f, 0, 5.0999999f);

    public SerializedVector3.SerializedVector3 GuardSpawner { get; set; } = new Vector3(4.9000001f, 0, 15.1999998f);

    public SerializedVector3.SerializedVector3 ScientistSpawner { get; set; } = new Vector3(-5.0999999f, 0, 15f);

    public SerializedVector3.SerializedVector3 ScpSpawner { get; set; } = new Vector3(8.39999962f, 0, 5f);
}


