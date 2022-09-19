using System;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

public class EventHandlers : Plugin<Config>
{
    public void OnHurting(HurtingEventArgs ev)
    {
        
        if (ManyTweaks.Singleton.Config.No207Dmg)
        {
            if (ev.Handler.Type == DamageType.Scp207)
            {
                ev.Amount = 0f;
                ev.IsAllowed = false;
            }
        }
    }
}
