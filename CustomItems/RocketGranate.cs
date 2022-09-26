// -----------------------------------------------------------------------
// <copyright file="EmpGrenade.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Roles;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using Exiled.Events.Handlers;
    using InventorySystem.Items.Firearms.Attachments;
    using InventorySystem.Items.Firearms.Attachments.Components;
    using MEC;
    using RemoteAdmin.Communication;
    using UnityEngine;
    using Item = Exiled.API.Features.Items.Item;
    using KeycardPermissions = Interactables.Interobjects.DoorUtils.KeycardPermissions;
    using Player = Exiled.API.Features.Player;

    /// <inheritdoc />
    [CustomItem(ItemType.GrenadeFlash)]
    public class RocketGranate : CustomGrenade
    {
        private static readonly List<Room> LockedRooms079 = new List<Room>();

        private readonly List<Door> lockedDoors = new List<Door>();

        private readonly List<TeslaGate> disabledTeslaGates = new List<TeslaGate>();

        /// <inheritdoc/>
        public override uint Id { get; set; } = 73;

        /// <inheritdoc/>
        public override string Name { get; set; } = "Rocket Granate";

        /// <inheritdoc/>
        public override float Weight { get; set; } = 1.15f;

        /// <inheritdoc/>
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {

            },
        };

        /// <inheritdoc/>
        public override string Description { get; set; } = "Is Funni.";

        /// <inheritdoc/>
        public override bool ExplodeOnCollision { get; set; } = true;

        /// <inheritdoc/>
        public override float FuseTime { get; set; } = 1.5f;


        /// <inheritdoc/>
        protected override void OnExploding(ExplodingGrenadeEventArgs ev)
        {
            foreach (Player player1 in ev.TargetsToAffect)
            {
                Timing.RunCoroutine(EventHandlers.DoRocket(player1, 1));
                ev.IsAllowed = false;
            }
           
        }

    }
}