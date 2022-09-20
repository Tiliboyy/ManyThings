using System;
using System.ComponentModel;
using Exiled.API.Interfaces;

public class Config : IConfig
{

    [Description("Enables the Plugin")]
    public bool IsEnabled { get; set; } = true;

    [Description("Disables 207 Damage")]
    public bool No207Dmg { get; set; } = false;

}
