﻿

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
    [CustomItem(ItemType.Adrenaline)]
    public class FOURTWEANTY : CustomItem
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 420;

        /// <inheritdoc/>
        public override string Name { get; set; } = "420J";

        /// <inheritdoc/>
        public override string Description { get; set; } = "Gibt dir 3 zufällige Effekte und einen Speedboost.";

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

            var effect = new List<EffectType>();
            effect.Add(EffectType.Poisoned);
            effect.Add(EffectType.RainbowTaste);
            effect.Add(EffectType.Visuals939);
            effect.Add(EffectType.SinkHole);
            effect.Add(EffectType.Invigorated);
            effect.Add(EffectType.Concussed);
            effect.Add(EffectType.Deafened);
            effect.Add(EffectType.Exhausted);
            effect.Add(EffectType.Invisible);


            var time = UnityEngine.Random.Range(1, 10);
            var rngeffect1 = UnityEngine.Random.Range(0, 7);
            var rngeffect2 = UnityEngine.Random.Range(0, 7);
            var rngeffect3 = UnityEngine.Random.Range(0, 7);
            ev.Player.ShowHint($"Du hast {effect[rngeffect1]}, {effect[rngeffect2]} und {effect[rngeffect3]} bekommen.", 3f);
            ev.Player.EnableEffect(effect[rngeffect1], time);
            ev.Player.EnableEffect(effect[rngeffect2], time);
            ev.Player.EnableEffect(effect[rngeffect3], time);
            ev.Player.EnableEffect(EffectType.MovementBoost, time + 2);

            ev.Player.ChangeEffectIntensity<MovementBoost>(75);
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }
    }
}