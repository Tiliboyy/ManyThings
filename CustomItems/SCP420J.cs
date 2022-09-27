/*
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using System.ComponentModel;
using Player = Exiled.Events.Handlers.Player;
using Random = UnityEngine.Random;




namespace ManyTweaks.Items
{


    /// <inheritdoc />
    [CustomItem(ItemType.Adrenaline)]
    public class FOURTWEANTY : CustomItem
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 420;

        /// <inheritdoc/>
        public override string Name { get; set; } = "420J";

        /// <inheritdoc/>
        public override string Description { get; set; } = "Gibt dir zufällige Effekte und einen Speedboost.";

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
            effect.Add(EffectType.Ensnared);
            effect.Add(EffectType.Stained);
            effect.Add(EffectType.Scp207);
            effect.Add(EffectType.Poisoned);






            var time = Random.Range(1, 10);
            string effects = string.Empty;
            string effecttype = string.Empty;
            foreach (var type in effect)
            {
                int rngsus = Random.Range(1, 10);
                if (rngsus < 2)
                {

                    ev.Player.EnableEffect(type, time);
                    Log.Info($"{ev.Player.Nickname} got {type} for {time} seconds.");
                    effects = type.ToString();
                    effecttype = effecttype + effects;
                    Log.Warn(effecttype);

                }
            }
            string effecttext = $"Du hast die Effekte {effecttype} erhalten.";
            Log.Info(effecttype);
            ev.Player.ShowHint(effecttext, 5);
            ev.Player.EnableEffect(EffectType.MovementBoost, time + 2);



            ev.Player.ChangeEffectIntensity<MovementBoost>(75);
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }
    }
}
*/