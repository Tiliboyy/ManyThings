using Exiled.API.Interfaces;
using System.ComponentModel;

public class Config : IConfig
{

    [Description("Enables the Plugin")]
    public bool IsEnabled { get; set; } = true;

    [Description("Enables Debug mode")]
    public bool IsDebug { get; set; } = false;

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


    [Description("Diese Nachrichten werden angezeigt, wenn der Spieler auf den Spawn Pads steht")]
    public string Scpmessage { get; set; } = "Du hast <color=#ff0509>SCP</color> gewählt!";
    public string Classdmessge { get; set; } = "Du hast <color=#ff7d05>Klasse-D</color> gewählt!";
    public string Guardmessage { get; set; } = "Du hast <color=#898889>Sicherheitspersonal</color> gewählt!";
    public string ScientistMessage { get; set; } = "Du hast <color=#ffee00>Wissenschaftler</color> gewählt!";

}

