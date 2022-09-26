// -----------------------------------------------------------------------
// <copyright file="GrenadeLauncher.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using InventorySystem.Items.Firearms.Attachments;
    using InventorySystem.Items.Firearms.BasicMessages;
    using InventorySystem.Items.ThrowableProjectiles;
    using MEC;
    using UnityEngine;
    using CollisionHandler = Exiled.API.Features.Components.CollisionHandler;

    /// <inheritdoc />
    [CustomItem(ItemType.GunLogicer)]
    public class BallLauncher : CustomWeapon
    {
        private CustomGrenade loadedCustomGrenade;

        private GrenadeType loadedGrenade = GrenadeType.FragGrenade;

        /// <inheritdoc/>
        public override uint Id { get; set; } = 72;

        /// <inheritdoc/>
        public override string Name { get; set; } = "Ball Launcher";

        /// <inheritdoc/>
        public override string Description { get; set; } = "This weapon will launch grenades in the direction you are firing, instead of bullets. Requires Frag Grenades in your inventory to reload.";

        /// <inheritdoc/>
        public override float Weight { get; set; } = 1f;

        /// <inheritdoc/>
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {

            },
        };

        /// <inheritdoc/>
        public override float Damage { get; set; }

        /// <inheritdoc/>
        public override byte ClipSize { get; set; } = 100;

        /// <summary>
        /// Gets or sets a value indicating whether or not players will need actual frag grenades in their inventory to use as ammo. If false, the weapon's base ammo type is used instead.
        /// </summary>
        [Description("Whether or not players will need actual frag grenades in their inventory to use as ammo. If false, the weapon's base ammo type is used instead.")]
        public bool UseGrenades { get; set; } = true;

        /// <summary>
        /// Gets or sets the speed of grenades when they shoot out of the weapon.
        /// </summary>
        [Description("The speed of grenades when they shoot out of the weapon.")]
        public float GrenadeSpeed { get; set; } = 1.5f;

        /// <summary>
        /// Gets or sets the max duration of the fuse of grenades shot from the weapon. Note, these grenades will always explode immediatly when they collide with something, but this can be used with slow-moving grenades to cause mid-air explosions.
        /// </summary>
        [Description("The max duration of the fuse of grenades shot from the weapon. Note, these grenades will always explode immediatly when they collide with something, but this can be used with slow-moving grenades to cause mid-air explosions.")]
        public float FuseTime { get; set; } = 1f;

        /// <summary>
        /// Gets or sets a value indicating whether the GL should ignore modded grenades.
        /// </summary>
        [Description("Whether or not the Grenade Launcher will consider modded frag grenades as viable grenades for reloading.")]
        public bool IgnoreModdedGrenades { get; set; } = false;

        /// <inheritdoc/>
        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            if (UseGrenades)
            {
                ev.IsAllowed = false;

                if (!(ev.Player.CurrentItem is Firearm firearm) || firearm.Ammo >= ClipSize)
                    return;

                foreach (Item item in ev.Player.Items.ToList())
                {
                    if (item.Type != ItemType.SCP2176 && item.Type != ItemType.GrenadeFlash && item.Type != ItemType.SCP018)
                        continue;
                    if (TryGet(item, out CustomItem cItem))
                    {
                        if (IgnoreModdedGrenades)
                            continue;

                        if (cItem is CustomGrenade customGrenade)
                            loadedCustomGrenade = customGrenade;
                    }

                    ev.Player.DisableEffect(EffectType.Invisible);
                    ev.Player.Connection.Send(new RequestMessage(ev.Firearm.Serial, RequestType.Reload));

                    Timing.CallDelayed(3f, () => firearm.Ammo = ClipSize);

                    loadedGrenade = item.Type == ItemType.GrenadeFlash ? GrenadeType.Flashbang :
                        item.Type == ItemType.SCP2176 ? GrenadeType.FragGrenade : GrenadeType.Scp018;
                    ev.Player.RemoveItem(item);

                    return;
                }

            }
        }

        /// <inheritdoc/>
        protected override void OnShooting(ShootingEventArgs ev)
        {
            ev.IsAllowed = false;

            Vector3 pos = ev.Shooter.CameraTransform.TransformPoint(new Vector3(0.0715f, 0.0225f, 0.45f));
            ThrownProjectile projectile;

            if (loadedCustomGrenade != null)
            {
                projectile = (ThrownProjectile)loadedCustomGrenade.Throw(pos, GrenadeSpeed, FuseTime, loadedCustomGrenade.Type, ev.Shooter).Base;
                loadedCustomGrenade = null;
            }
            else
            {
                switch (loadedGrenade)
                {
                    default:
                        projectile = ev.Shooter.ThrowGrenade(GrenadeType.Scp018).Base.Projectile;
                        break;
                }
            }

            var comp = projectile.gameObject.AddComponent<CollisionHandler>();
            comp.Init(ev.Shooter.GameObject, projectile);
            if (comp.Owner == null || comp.Grenade == null)
            {
                Log.Error("CollisionHandler component failed to initialize!");
                return;
            }
        }
    }
}