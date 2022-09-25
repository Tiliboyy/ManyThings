

namespace CustomItems.Items
{
    using CustomPlayerEffects;
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using UnityEngine;
    using Player = Exiled.Events.Handlers.Player;

    /// <inheritdoc />
    [CustomItem(ItemType.Painkillers)]
    public class ZombiePille : CustomItem
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 68;

        /// <inheritdoc/>
        public override string Name { get; set; } = "Zombie Pille";

        /// <inheritdoc/>
        public override string Description { get; set; } = "Macht dich ein Zombie.";

        /// <inheritdoc/>
        public override float Weight { get; set; } = 1f;

        /// <inheritdoc/>
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 0,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {

            },
        };

        /// <summary>
        /// Gets or sets a value indicating whether the Lethal Injection should always kill the user, regardless of if they stop SCP-096's enrage.
        /// </summary>
        [Description("Makes you high.")]

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            Player.UsingItem += OnUsingItem;

            base.SubscribeEvents();
        }

        /// <inheritdoc/>
        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsingItem;

            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsedItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
            {
                return;
            }
            ev.Player.SetRole(RoleType.Scp0492,SpawnReason.ForceClass, true);
        }
    }
}