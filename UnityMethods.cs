using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Firearms.Ammo;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityMethods

{
    class UnityMethods
    {
        public static IEnumerator<float> DensifyAmmoBoxes(DroppingAmmoEventArgs ev)
        {
            yield return Timing.WaitForSeconds(0.2f);

            bool AmmoBoxes = false;
            List<Pickup> Ammo9 = new List<Pickup>();
            List<Pickup> Ammo556 = new List<Pickup>();
            List<Pickup> Ammo762 = new List<Pickup>();
            List<Pickup> Ammo44 = new List<Pickup>();
            List<Pickup> Ammo12 = new List<Pickup>();


            foreach (var pickup in Map.Pickups)
            {
                if (Vector3.Distance(ev.Player.Position, pickup.Position) < 4)
                {
                    switch (pickup.Type)
                    {
                        case ItemType.Ammo9x19:
                            Ammo9.Add(pickup);
                            AmmoBoxes = true;
                            Log.Info("9 mm");
                            break;

                        case ItemType.Ammo12gauge:
                            Ammo12.Add(pickup);
                            AmmoBoxes = true;
                            Log.Info("12 gauge");
                            break;

                        case ItemType.Ammo44cal:
                            Ammo44.Add(pickup);
                            AmmoBoxes = true;
                            Log.Info("44 cal");
                            break;

                        case ItemType.Ammo556x45:
                            Ammo556.Add(pickup);
                            AmmoBoxes = true;
                            Log.Info("556 ammo");
                            break;

                        case ItemType.Ammo762x39:
                            Ammo762.Add(pickup);
                            AmmoBoxes = true;
                            Log.Info("762 ammo");
                            break;
                    }
                }
            }

            if (!AmmoBoxes) yield break;
            // 9mm
            if (Ammo9.Count > 1)
            {
                Vector3 MeanVector = Vector3.zero;

                foreach (var box in Ammo9)
                    MeanVector += box.Position;

                MeanVector = MeanVector / Ammo9.Count;


                Pickup AuthorityBox = Ammo9[0];
                foreach (var box in Ammo9)
                    if (Vector3.Distance(box.Position, MeanVector) <
                        Vector3.Distance(AuthorityBox.Position, MeanVector))
                        AuthorityBox = box;

                Ammo9.Remove(AuthorityBox);

                AmmoPickup AuthorityAmmoPickup = (AmmoPickup)AuthorityBox.Base;

                ushort Amount = 0;
                foreach (var box in Ammo9)
                {
                    var a = (AmmoPickup)box.Base;
                    Amount += a.NetworkSavedAmmo;
                }

                AuthorityAmmoPickup.NetworkSavedAmmo += Amount;

                foreach (var box in Ammo9)
                {
                    box.Destroy();
                }
            }

            // 5.56mm
            if (Ammo556.Count > 1)
            {
                Vector3 MeanVector = Vector3.zero;

                foreach (var box in Ammo556)
                    MeanVector += box.Position;

                MeanVector /= Ammo9.Count;


                Pickup AuthorityBox = Ammo556[0];
                foreach (var box in Ammo556)
                    if (Vector3.Distance(box.Position, MeanVector) <
                        Vector3.Distance(AuthorityBox.Position, MeanVector))
                        AuthorityBox = box;

                Ammo556.Remove(AuthorityBox);

                AmmoPickup AuthorityAmmoPickup = (AmmoPickup)AuthorityBox.Base;

                ushort Amount = 0;
                foreach (var box in Ammo556)
                {
                    var a = (AmmoPickup)box.Base;
                    Amount += a.NetworkSavedAmmo;
                }

                AuthorityAmmoPickup.NetworkSavedAmmo += Amount;

                foreach (var box in Ammo556)
                {
                    box.Destroy();
                }
            }

            // 7.62mm
            if (Ammo762.Count > 1)
            {
                Vector3 MeanVector = Vector3.zero;

                foreach (var box in Ammo762)
                    MeanVector += box.Position;

                MeanVector = MeanVector / Ammo762.Count;


                Pickup AuthorityBox = Ammo762[0];
                foreach (var box in Ammo762)
                    if (Vector3.Distance(box.Position, MeanVector) <
                        Vector3.Distance(AuthorityBox.Position, MeanVector))
                        AuthorityBox = box;

                Ammo762.Remove(AuthorityBox);

                AmmoPickup AuthorityAmmoPickup = (AmmoPickup)AuthorityBox.Base;

                ushort Amount = 0;
                foreach (var box in Ammo762)
                {
                    var a = (AmmoPickup)box.Base;
                    Amount += a.NetworkSavedAmmo;
                }

                AuthorityAmmoPickup.NetworkSavedAmmo += Amount;

                foreach (var box in Ammo762)
                {
                    box.Destroy();
                }
            }

            // .44 cal
            if (Ammo44.Count > 1)
            {
                Vector3 MeanVector = Vector3.zero;

                foreach (var box in Ammo44)
                    MeanVector += box.Position;

                MeanVector = MeanVector / Ammo44.Count;


                Pickup AuthorityBox = Ammo44[0];
                foreach (var box in Ammo44)
                    if (Vector3.Distance(box.Position, MeanVector) <
                        Vector3.Distance(AuthorityBox.Position, MeanVector))
                        AuthorityBox = box;

                Ammo44.Remove(AuthorityBox);

                AmmoPickup AuthorityAmmoPickup = (AmmoPickup)AuthorityBox.Base;

                ushort Amount = 0;
                foreach (var box in Ammo44)
                {
                    var a = (AmmoPickup)box.Base;
                    Amount += a.NetworkSavedAmmo;
                }

                AuthorityAmmoPickup.NetworkSavedAmmo += Amount;

                foreach (var box in Ammo44)
                {
                    box.Destroy();
                }
            }

            // 12 Guage
            if (Ammo12.Count > 1)
            {
                Vector3 MeanVector = Vector3.zero;

                foreach (var box in Ammo12)
                    MeanVector += box.Position;

                MeanVector = MeanVector / Ammo12.Count;


                Pickup AuthorityBox = Ammo12[0];
                foreach (var box in Ammo12)
                    if (Vector3.Distance(box.Position, MeanVector) <
                        Vector3.Distance(AuthorityBox.Position, MeanVector))
                        AuthorityBox = box;

                Ammo12.Remove(AuthorityBox);

                AmmoPickup AuthorityAmmoPickup = (AmmoPickup)AuthorityBox.Base;

                ushort Amount = 0;
                foreach (var box in Ammo12)
                {
                    var a = (AmmoPickup)box.Base;
                    Amount += a.NetworkSavedAmmo;
                }

                AuthorityAmmoPickup.NetworkSavedAmmo += Amount;

                foreach (var box in Ammo12)
                {
                    box.Destroy();
                }
            }
        }
    }
}
