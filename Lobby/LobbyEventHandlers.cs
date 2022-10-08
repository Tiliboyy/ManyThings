﻿using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using GameCore;
using ManyThings.LobbySpawner;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MEC;
using Mirror.LiteNetLib4Mirror;
using Mirror;
using RemoteAdmin;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;
using Random = UnityEngine.Random;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using HarmonyLib;
using Assets._Scripts.Dissonance;
using System.Xml.Linq;

namespace ManyThings.Lobby
{
    public class LobbyEventHandlers : Plugin<Config>
    {

        public static CoroutineHandle LobbyTimer;

        public SchematicObject lobby;

        public static System.Random random = new System.Random();

        public static Vector3 SpawnRotation = Plugin.Instance.Config.SpawnRotation;

        private List<GameObject> Dummies = new List<GameObject> { };

        public static Vector3 ClassDPoint = Plugin.Instance.Config.ClassDSpawner;
        public static Vector3 GuardPoint = Plugin.Instance.Config.GuardSpawner;
        public static Vector3 SCPPoint = Plugin.Instance.Config.ScpSpawner;
        public static Vector3 ScientistPoint = Plugin.Instance.Config.ScientistSpawner;

        public static Vector3 SpawnPoint = Plugin.Instance.Config.SpawnPoint;

        public void OnRoundStart()
        {
            if (LobbyTimer.IsRunning)
            {
                Timing.KillCoroutines(LobbyTimer);
            }
            if (lobby != null)
            {
                lobby.Destroy();
            }
            foreach (Player player in Player.List)
            {
                player.ClearInventory();
                player.Role.Type = RoleType.Spectator;
            }


            List<Player> BulkList = Player.List.ToList();
            List<Player> SCPPlayers = new List<Player> { };
            List<Player> ScientistPlayers = new List<Player> { };
            List<Player> GuardPlayers = new List<Player> { };
            List<Player> ClassDPlayers = new List<Player> { };

            List<Player> PlayersToSpawnAsSCP = new List<Player> { };
            List<Player> PlayersToSpawnAsScientist = new List<Player> { };
            List<Player> PlayersToSpawnAsGuard = new List<Player> { };
            List<Player> PlayersToSpawnAsClassD = new List<Player> { };

            int SCPsToSpawn = 0;
            int ClassDsToSpawn = 0;
            int ScientistsToSpawn = 0;
            int GuardsToSpawn = 0;

            List<char> SpawnSequence = new List<char>
                { '4', '0', '1', '4', '3', '1', '4', '0', '3', '1', '4', '4', '1', '4', '0', '4', '1', '3', '4', '0', '3', '1', '4', '4', '1', '4', '0', '4', '1', '3', '0', '4', '4', '1', '0', '1', '4', '3', '3', '1' };

            for (int x = 0; x < Player.List.ToList().Count; x++)
            {
                switch (SpawnSequence[x])
                {
                    case '4':
                        ClassDsToSpawn += 1;
                        break;
                    case '3':
                        ScientistsToSpawn += 1;
                        break;
                    case '1':
                        GuardsToSpawn += 1;
                        break;
                    case '0':
                        SCPsToSpawn += 1;
                        break;
                }
            }

            foreach (var player in Player.List)
            {

                if (Vector3.Distance(player.Position, SpawnPoint + Plugin.Instance.Config.ScpSpawner) <= 3.4)
                {
                    SCPPlayers.Add(player);
                    Log.Debug($"SCP1: {player}", Plugin.Instance.Config.IsDebug);
                }
                else if (Vector3.Distance(player.Position, SpawnPoint + Plugin.Instance.Config.ClassDSpawner) <= 3.4)
                {
                    ClassDPlayers.Add(player);
                    Log.Debug($"ClassD1: {player}", Plugin.Instance.Config.IsDebug);
                }
                else if (Vector3.Distance(player.Position, SpawnPoint + Plugin.Instance.Config.ScientistSpawner) <= 3.4)
                {
                    ScientistPlayers.Add(player);
                    Log.Debug($"Scientist1: {player}", Plugin.Instance.Config.IsDebug);
                }
                else if (Vector3.Distance(player.Position, SpawnPoint + Plugin.Instance.Config.GuardSpawner) <= 3.4)
                {
                    GuardPlayers.Add(player);
                    Log.Debug($"Guard1: {player}", Plugin.Instance.Config.IsDebug);
                }
                player.Role.Type = RoleType.None;
            }


            if (ClassDsToSpawn != 0)
            {
                if (ClassDPlayers.Count <= ClassDsToSpawn)
                {
                    foreach (Player ply in ClassDPlayers)
                    {
                        PlayersToSpawnAsClassD.Add(ply);
                        ClassDsToSpawn -= 1;
                        BulkList.Remove(ply);
                    }
                }
                else
                {
                    for (int x = 0; x < ClassDsToSpawn; x++)
                    {
                        Player Ply = ClassDPlayers[random.Next(ClassDPlayers.Count)];
                        PlayersToSpawnAsClassD.Add(Ply);
                        ClassDPlayers.Remove(Ply); // Removing winner from the list
                        BulkList.Remove(Ply); // Removing the winners from the bulk list
                    }
                    ClassDsToSpawn = 0;
                }
            }


            if (ScientistsToSpawn != 0)
            {
                if (ScientistPlayers.Count <= ScientistsToSpawn)
                {
                    foreach (Player ply in ScientistPlayers)
                    {
                        PlayersToSpawnAsScientist.Add(ply);
                        ScientistsToSpawn -= 1;
                        BulkList.Remove(ply);
                    }
                }
                else
                {
                    for (int x = 0; x < ScientistsToSpawn; x++)
                    {
                        Player Ply = ScientistPlayers[random.Next(ScientistPlayers.Count)];
                        PlayersToSpawnAsScientist.Add(Ply);
                        ScientistPlayers.Remove(Ply);
                        BulkList.Remove(Ply);
                    }
                    ScientistsToSpawn = 0;
                }
            }


            if (GuardsToSpawn != 0)
            {
                if (GuardPlayers.Count <= GuardsToSpawn)
                {
                    foreach (Player ply in GuardPlayers)
                    {
                        PlayersToSpawnAsGuard.Add(ply);
                        GuardsToSpawn -= 1;
                        BulkList.Remove(ply);
                    }
                }
                else
                {
                    for (int x = 0; x < GuardsToSpawn; x++)
                    {
                        Player Ply = GuardPlayers[random.Next(GuardPlayers.Count)];
                        PlayersToSpawnAsGuard.Add(Ply);
                        GuardPlayers.Remove(Ply);
                        BulkList.Remove(Ply);
                    }
                    GuardsToSpawn = 0;
                }
            }

            if (SCPsToSpawn != 0)
            {
                if (SCPPlayers.Count <= SCPsToSpawn)
                {
                    foreach (Player ply in SCPPlayers)
                    {
                        PlayersToSpawnAsSCP.Add(ply);
                        SCPsToSpawn -= 1;
                        BulkList.Remove(ply);
                    }
                }
                else
                {
                    for (int x = 0; x < SCPsToSpawn; x++)
                    {
                        Player Ply = SCPPlayers[random.Next(SCPPlayers.Count)];
                        SCPPlayers.Remove(Ply);
                        PlayersToSpawnAsSCP.Add(Ply);
                        BulkList.Remove(Ply);
                    }
                    SCPsToSpawn = 0;
                }
            }
            if (ClassDsToSpawn != 0)
            {
                for (int x = 0; x < ClassDsToSpawn; x++)
                {
                    Player Ply = BulkList[random.Next(BulkList.Count)];
                    PlayersToSpawnAsClassD.Add(Ply);
                    BulkList.Remove(Ply);
                }
            }
            if (SCPsToSpawn != 0)
            {
                for (int x = 0; x < SCPsToSpawn; x++)
                {
                    Player Ply = BulkList[random.Next(BulkList.Count)];
                    PlayersToSpawnAsSCP.Add(Ply);
                    BulkList.Remove(Ply);
                }
            }
            if (ScientistsToSpawn != 0)
            {
                for (int x = 0; x < ScientistsToSpawn; x++)
                {
                    Player Ply = BulkList[random.Next(BulkList.Count)];
                    PlayersToSpawnAsScientist.Add(Ply);
                    BulkList.Remove(Ply);
                }
            }
            if (GuardsToSpawn != 0)
            {
                for (int x = 0; x < GuardsToSpawn; x++)
                {
                    Player Ply = BulkList[random.Next(BulkList.Count)];
                    PlayersToSpawnAsGuard.Add(Ply);
                    BulkList.Remove(Ply);
                }
            }

            foreach (Player ply in PlayersToSpawnAsClassD)
            {
                Timing.CallDelayed(0.1f, () =>
                {
                    ply.Role.Type = RoleType.ClassD;
                });
            }
            foreach (Player ply in PlayersToSpawnAsScientist)
            {
                Timing.CallDelayed(0.1f, () =>
                {
                    ply.Role.Type = RoleType.Scientist;
                });
            }
            foreach (Player ply in PlayersToSpawnAsGuard)
            {
                Timing.CallDelayed(0.1f, () =>
                {
                    ply.Role.Type = RoleType.FacilityGuard;
                });
            }

            List<RoleType> Roles = new List<RoleType>
                { RoleType.Scp049, RoleType.Scp096, RoleType.Scp106, RoleType.Scp173, RoleType.Scp93953, RoleType.Scp93989 };

            if (PlayersToSpawnAsSCP.Count > 2)
                Roles.Add(RoleType.Scp079);

            foreach (Player ply in PlayersToSpawnAsSCP)
            {
                RoleType role = Roles[random.Next(Roles.Count)];
                Roles.Remove(role);

                Timing.CallDelayed(0.1f, () =>
                {
                    ply.Role.Type = role;
                });
            }

        }
        public void WaitingForPlayers()
        {
            if (Round.IsStarted)
                return;
            Dictionary<RoleType, string> dummiesToSpawn = new Dictionary<RoleType, string>
            {
                { RoleType.ClassD, "a" },
                { RoleType.Scp173, "a" },
                { RoleType.Scientist, "a" },
                { RoleType.FacilityGuard, "a" },
            };
            Dictionary<RoleType, KeyValuePair<Vector3, Quaternion>> dummySpawnPointsAndRotations = new Dictionary<RoleType, KeyValuePair<Vector3, Quaternion>>
            {
                { RoleType.Scientist, new KeyValuePair<Vector3, Quaternion>(LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.ScientistSpawner + Vector3.up, Quaternion.Euler(0,129.25f , 0) ) },
                { RoleType.Scp173, new KeyValuePair<Vector3, Quaternion>(LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.ScpSpawner + Vector3.up, Quaternion.Euler(0, 100.64f, 0f) ) },
                { RoleType.FacilityGuard, new KeyValuePair<Vector3, Quaternion>(LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.GuardSpawner + Vector3.up, Quaternion.Euler(0f, 12f, 0f) ) },
                { RoleType.ClassD, new KeyValuePair<Vector3, Quaternion>(LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.ClassDSpawner + Vector3.up, Quaternion.Euler(0, 340f, 0) ) },
            };
            foreach (var Role in dummiesToSpawn)
            {
                GameObject obj = UnityEngine.Object.Instantiate(LiteNetLib4MirrorNetworkManager.singleton.playerPrefab);
                CharacterClassManager ccm = obj.GetComponent<CharacterClassManager>();
                if (ccm == null)
                    Log.Error("CCM is null, this can cause problems!");
                ccm.CurClass = Role.Key;
                ccm.GodMode = true;
                //ccm.OldRefreshPlyModel(PlayerManager.localPlayer);
                obj.GetComponent<NicknameSync>().Network_myNickSync = Role.Value;
                obj.GetComponent<QueryProcessor>().PlayerId = 9999;
                obj.GetComponent<QueryProcessor>().NetworkPlayerId = 9999;
                obj.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);

                obj.transform.position = dummySpawnPointsAndRotations[Role.Key].Key;
                obj.transform.rotation = dummySpawnPointsAndRotations[Role.Key].Value;

                NetworkServer.Spawn(obj);
                Dummies.Add(obj);
                Log.Debug($"Spawned dummy {Role.Key} at {obj.transform.position}");
                Log.Debug(Dummies.Count.ToString());
            }
            int Lobbynum;
            if (Plugin.Instance.Config.LobbySchematics.Count == 0)
            {
                Log.Warn("No Lobby in Config");
                Lobbynum = 0;
            }
            else
            {
                Lobbynum = Random.Range(0, Plugin.Instance.Config.LobbySchematics.Count - 1);
            }
            lobby = ObjectSpawner.SpawnSchematic(Plugin.Instance.Config.LobbySchematics[Lobbynum], SpawnPoint, Quaternion.identity);
 
            Log.Debug($"Lobby: {lobby}", Plugin.Instance.Config.IsDebug);
            Log.Debug($"Lobbynum: {Lobbynum}", Plugin.Instance.Config.IsDebug);
            Log.Debug("LobbyCount: " + Plugin.Instance.Config.LobbySchematics.Count, Plugin.Instance.Config.IsDebug);
            Log.Debug("Lobby0: " + Plugin.Instance.Config.LobbySchematics[0], Plugin.Instance.Config.IsDebug);
            #region Ugly Code
            var GameObject1 = new GameObject("Spawner1");
            var Collider1 = GameObject1.AddComponent<SphereCollider>();
            Collider1.isTrigger = true;
            Collider1.radius = 3.4f;
            GameObject1.AddComponent<ScpSpawner>();
            GameObject1.transform.position = LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.ScpSpawner;

            var GameObject2 = new GameObject("Spawner2");
            var Collider2 = GameObject2.AddComponent<SphereCollider>();
            Collider2.isTrigger = true;
            Collider2.radius = 3.4f;
            GameObject2.AddComponent<ClassDSpawner>();
            GameObject2.transform.position = LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.ClassDSpawner;

            var GameObject3 = new GameObject("Spawner3");
            var Collider3 = GameObject3.AddComponent<SphereCollider>();
            Collider3.isTrigger = true;
            Collider3.radius = 3.4f;
            GameObject3.AddComponent<ScientistSpawner>();
            GameObject3.transform.position = LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.ScientistSpawner;

            var GameObject4 = new GameObject("Spawner4");
            var Collider4 = GameObject4.AddComponent<SphereCollider>();
            Collider4.isTrigger = true;
            Collider4.radius = 3.4f;
            GameObject4.AddComponent<GuardSpawner>();
            GameObject4.transform.position = LobbyEventHandlers.SpawnPoint + Plugin.Instance.Config.GuardSpawner;


            #endregion

            GameObject.Find("StartRound").transform.localScale = Vector3.zero;

            LobbyTimer = Timing.RunCoroutine(LobbyMethods.LobbyTimer());

            
        }
        public void VerifiedPlayer(VerifiedEventArgs ev)
        {

            if (!Round.IsStarted && (GameCore.RoundStart.singleton.NetworkTimer > 1 || GameCore.RoundStart.singleton.NetworkTimer == -2))
            {
                if (!Plugin.Instance.Config.GlobalVoiceChat)
                {
                    MirrorExtensions.SendFakeSyncVar(ev.Player, RoundStart.singleton.netIdentity, typeof(RoundStart), "NetworkTimer", -1);
                    Log.Debug("Executed FakeSyncVar", Plugin.Instance.Config.IsDebug);
                }


                Timing.CallDelayed(Plugin.Instance.Config.SpawnDelay, () =>
                {

                    ev.Player.Role.Type = Config.RolesToChoose[Random.Range(0, Config.RolesToChoose.Count)];
                    Player player = ev.Player;
                });

            }


        }
        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (!Round.IsStarted)
            {
                ev.Player.ClearInventory();
                ev.Player.Ammo.Clear();
                ev.Player.Position = SpawnPoint + Vector3.up;
                ev.Player.Rotation = new Vector3(SpawnRotation.x, SpawnRotation.y, SpawnRotation.z);
                foreach (var ammo in Config.Ammo)
                {
                    ev.Player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                }
                foreach (ItemType item in Plugin.Instance.Config.LobbyItems)
                {
                    ev.Player.AddItem(item);
                }


            }

        }
        public void OnDied(DiedEventArgs ev)
        {
            if (!Round.IsStarted)
            {
                Timing.CallDelayed(2f, () =>
                {
                    if (!Round.IsStarted)
                    {
                        Player player = ev.Target;
                        ev.Target.Role.Type = Config.RolesToChoose[Random.Range(0, Config.RolesToChoose.Count)];
                    }
                });
            }



        }
        public void OnThrow(ThrowingItemEventArgs ev)
        {
            if (Plugin.Instance.Config.AllowDroppingItem == false)
            {
                if (!Round.IsStarted)
                {
                    ev.IsAllowed = false;
                }
            }

        }
        public void OnDrop(DroppingItemEventArgs ev)
        {
            if (Plugin.Instance.Config.AllowDroppingItem == false)
            {
                if (!Round.IsStarted)
                {
                    ev.IsAllowed = false;
                }
            }

        }
        public void OnPlacingBlood(PlacingBloodEventArgs ev)
        {
            if (!Round.IsStarted)
            {
                ev.IsAllowed = false;
            }
        }

    }
}
