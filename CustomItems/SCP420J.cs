

namespace CustomItems.Items
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using CustomPlayerEffects;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using PlayableScps;
    using PlayerStatsSystem;
    using UnityEngine;
    using Player = Exiled.Events.Handlers.Player;
    using Scp096 = PlayableScps.Scp096;
    using Exiled.Events;

    /// <inheritdoc />
    [CustomItem(ItemType.Adrenaline)]
    public class FOURTWEANTY : CustomItem
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 420;

        /// <inheritdoc/>
        public override string Name { get; set; } = "420J";

        /// <inheritdoc/>
        public override string Description { get; set; } = "High Boii";

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


            var time = UnityEngine.Random.Range(5, 10);
            ev.Player.ApplyRandomEffect(time);
            ev.Player.ApplyRandomEffect(time);
            ev.Player.ApplyRandomEffect(time);
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }
    }
}