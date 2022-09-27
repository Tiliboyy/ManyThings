/*
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using System.ComponentModel;
using YamlDotNet.Serialization;




namespace ManyTweaks.Items
{

    /// <inheritdoc />
    [CustomItem(ItemType.GunE11SR)]
    public class RocketGun : CustomWeapon
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 69;

        /// <inheritdoc/>
        public override string Name { get; set; } = "Rocket Gun";

        /// <inheritdoc/>
        public override string Description { get; set; } = "Is Funni";

        /// <inheritdoc/>
        public override float Weight { get; set; } = 0f;

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
        /// 

        protected override void OnShot(ShotEventArgs ev)
        {
            Player player = ev.Target;
            Timing.RunCoroutine(EventHandlers.DoRocket(player, 1));
        }
        protected override void OnHurting(HurtingEventArgs ev)
        {

        }
    }
}
*/