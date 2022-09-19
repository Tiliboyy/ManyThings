namespace CustomItems.Items
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using InventorySystem.Items.Firearms.Attachments;
    using MEC;
    using PlayerStatsSystem;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    [CustomItem(ItemType.GunE11SR)]
    public class RocketGun : CustomWeapon
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 69;

        /// <inheritdoc/>
        public override string Name { get; set; } = "RocketGun";

        /// <inheritdoc/>
        public override string Description { get; set; } = "Uiiii";

        /// <inheritdoc/>
        public override float Weight { get; set; } = 3.25f;

        /// <inheritdoc/>
        public override byte ClipSize { get; set; } = 100;

        /// <inheritdoc/>
        public override bool ShouldMessageOnGban { get; } = true;

        /// <inheritdoc/>
        [YamlIgnore]
        public override float Damage { get; set; }

        /// <inheritdoc/>
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 0,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {

            },
        };

        /// <inheritdoc />
        [YamlIgnore]
        public override AttachmentName[] Attachments { get; set; } = new[]
        {
            AttachmentName.ExtendedBarrel,
            AttachmentName.ScopeSight,
        };

        /// <summary>
        /// Gets or sets the amount of extra damage this weapon does, as a multiplier.
        /// </summary>
        [Description("The amount of extra damage this weapon does, as a multiplier.")]
        public float DamageMultiplier { get; set; } = 0f;

        /// <inheritdoc/>
        protected override void OnHurting(HurtingEventArgs ev)
        {
            Player player = ev.Target;
            if (ev.Attacker != ev.Target && ev.Handler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
                Timing.RunCoroutine(EventHandlers.DoRocket(player, 1));

        }
    }
}