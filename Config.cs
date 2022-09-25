using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.Commands.Reload;
using Exiled.Loader;
using SerializedVector3;
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

    public bool UseHints { get; set; } = true;

    [Description("Determines the position of the Hint on the users screen (32 = Top, 0 = Middle, -15 = Below)")]
    public int HintVertPos { get; set; } = 25;

    public string TopMessage { get; set; } = "<size=40><color=yellow><b>Die Runde startet bald, {seconds}</b></color></size>";

    public string BottomMessage { get; set; } = "<size=30><i>{players}</i></size>";

    public string ServerIsPaused { get; set; } = "Server ist Pausiert";

    public string RoundIsBeingStarted { get; set; } = "Die Runde startet";

    public string OneSecondRemain { get; set; } = "Sekunde";

    public string XSecondsRemains { get; set; } = "Sekunden";

    public string OnePlayerConnected { get; set; } = "Spieler ist verbunden";

    public string XPlayersConnected { get; set; } = "Spieler sind verbunden";

    public List<RoleType> RolesToChoose { get; private set; } = new List<RoleType>()
        {
            RoleType.Tutorial,
            RoleType.ChaosMarauder,
            RoleType.NtfCaptain,
        };

    [Description("Diese Nachrichten werden angezeigt, wenn der Spieler auf den Spawn Pads steht")]
    public string Scpmessage { get; set; } = "Du hast <color=#ff0509>SCP</color> gewählt!";
    public string Classdmessge { get; set; } = "Du hast <color=#ff7d05>Klasse-D</color> gewählt!";
    public string Guardmessage { get; set; } = "Du hast <color=#898889>Sicherheitspersonal</color> gewählt!";
    public string ScientistMessage { get; set; } = "Du hast <color=#ffee00>Wissenschaftler</color> gewählt!";

    public SerializedVector3.SerializedVector3 ClassDSpawner { get; set; } = new Vector3(8.4f, 0, 5.0f);

    public SerializedVector3.SerializedVector3 GuardSpawner { get; set; } = new Vector3(-8.4f, 0, 5.1f);

    public SerializedVector3.SerializedVector3 ScientistSpawner { get; set; } = new Vector3(-5.1f, 0, 15.0f);

    public SerializedVector3.SerializedVector3 ScpSpawner { get; set; } = new Vector3(5.0f, 0, 14.9f);
}


