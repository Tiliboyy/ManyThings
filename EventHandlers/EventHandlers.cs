using System;
using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using UnityEngine;
using MEC;
using Exiled.Events.Handlers;
using Player = Exiled.API.Features.Player;
using Item = Exiled.API.Features.Items.Item;

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
    public static IEnumerator<float> DoRocket(Player player, float speed)
    {
        const int maxAmnt = 50;
        int amnt = 0;
        while (player.Role != RoleType.Spectator)
        {
            player.Position += Vector3.up * speed;
            amnt++;
            if (amnt >= maxAmnt)
            {
                player.IsGodModeEnabled = false;
                ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = 0.5f;
                grenade.SpawnActive(player.Position, player);
                player.Kill("Went on a trip in their favorite rocket ship.");
            }

            yield return Timing.WaitForOneFrame;
        }
    }


}
