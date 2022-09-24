using System;
using System.ComponentModel;
using Exiled.API.Interfaces;

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

    public string TopMessage { get; set; } = "<size=40><color=yellow><b>The game will be starting soon, {seconds}</b></color></size>";

    public string BottomMessage { get; set; } = "<size=30><i>{players}</i></size>";

    public string ServerIsPaused { get; set; } = "Server is paused";

    public string RoundIsBeingStarted { get; set; } = "Round is being started";

    public string OneSecondRemain { get; set; } = "second remain";

    public string XSecondsRemains { get; set; } = "seconds remains";

    public string OnePlayerConnected { get; set; } = "player has connected";

    public string XPlayersConnected { get; set; } = "players have connected";
}

