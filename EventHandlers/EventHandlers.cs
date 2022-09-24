using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.API.Features.Items;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using Log = Exiled.API.Features.Log;
using ManyTweaksLobby;
using Exiled.API.Extensions;
using GameCore;

public class EventHandlers : Plugin<Config>
{

    public static CoroutineHandle LobbyTimer;

    public SchematicObject lobby;
    
    public static System.Random random = new System.Random();

    public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();


    public static Vector3 SpawnPoint = new Vector3(240.1f, 977, 95.8f);
    public void OnHurting(HurtingEventArgs ev)
    {

        if (ManyTweaks.Singleton.Config.No207Dmg)
        {
            if (ev.Handler.Type == DamageType.Scp207)
            {
                ev.Amount = 0f;
                ev.IsAllowed = false;
            }
        }
    }


    public static IEnumerator<float> DoRocket(Player player, float speed)
    {
        const int maxAmnt = 50;
        int amnt = 0;
        while (player.Role != RoleType.Spectator)
        {
            player.Position += Vector3.up * speed;
            amnt++;
            if (amnt >= maxAmnt)
            {
                player.IsGodModeEnabled = false;
                ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = 0.5f;
                grenade.SpawnActive(player.Position, player);
                player.Kill("Went on a trip in their favorite rocket ship.");
            }

            yield return Timing.WaitForOneFrame;
        }
    }
    public void OnDroppingAmmo(DroppingAmmoEventArgs ev)
    {
        if (ManyTweaks.Singleton.Config.AntiLag)
        {
            Timing.RunCoroutine(UnityMethods.UnityMethods.DensifyAmmoBoxes(ev));
        }
    }
    public void OnRoundStart()
    {
        if (lobby != null)
            lobby.Destroy();

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

            if (Vector3.Distance(player.Position, SpawnPoint + new Vector3(-7.935f, 0, -13.74f)) <= 3)
            {
                SCPPlayers.Add(player);
                Log.Info($"SCP1: {player}");
            }
            else if (Vector3.Distance(player.Position, SpawnPoint + new Vector3(13.74382f, 0, -7.935f)) <= 3)
            {
                ClassDPlayers.Add(player);
                Log.Info($"ClassD1: {player}");
            }
            else if (Vector3.Distance(player.Position, SpawnPoint + new Vector3(-13.74382f, 0, -7.935f)) <= 3)
            {
                ScientistPlayers.Add(player);
                Log.Info($"Scientist1: {player}");
            }
            else if (Vector3.Distance(player.Position, SpawnPoint + new Vector3(7.934999f, 0, -13.74382f)) <= 3)
            {
                GuardPlayers.Add(player);
                Log.Info($"Guard1: {player}");
            }
            player.Role.Type = RoleType.None;
        }

        // ---------------------------------------------------------------------------------------\\
        // ClassD
        if (ClassDsToSpawn != 0)
        {
            if (ClassDPlayers.Count <= ClassDsToSpawn) // Less people (or equal) voted than what is required in the game.
            {
                foreach (Player ply in ClassDPlayers)
                {
                    PlayersToSpawnAsClassD.Add(ply);
                    ClassDsToSpawn -= 1;
                    BulkList.Remove(ply);
                }
            }
            else // More people voted than what is required, time to play the game of chance.
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

        // ---------------------------------------------------------------------------------------\\
        // Scientists
        if (ScientistsToSpawn != 0)
        {
            if (ScientistPlayers.Count <= ScientistsToSpawn) // Less people (or equal) voted than what is required in the game.
            {
                foreach (Player ply in ScientistPlayers)
                {
                    PlayersToSpawnAsScientist.Add(ply);
                    ScientistsToSpawn -= 1;
                    BulkList.Remove(ply);
                }
            }
            else // More people voted than what is required, time to play the game of chance.
            {
                for (int x = 0; x < ScientistsToSpawn; x++)
                {
                    Player Ply = ScientistPlayers[random.Next(ScientistPlayers.Count)];
                    PlayersToSpawnAsScientist.Add(Ply);
                    ScientistPlayers.Remove(Ply); // Removing winner from the list
                    BulkList.Remove(Ply); // Removing the winners from the bulk list
                }
                ScientistsToSpawn = 0;
            }
        }

        // ---------------------------------------------------------------------------------------\\
        // Guards
        if (GuardsToSpawn != 0)
        {
            if (GuardPlayers.Count <= GuardsToSpawn) // Less people (or equal) voted than what is required in the game.
            {
                foreach (Player ply in GuardPlayers)
                {
                    PlayersToSpawnAsGuard.Add(ply);
                    GuardsToSpawn -= 1;
                    BulkList.Remove(ply);
                }
            }
            else // More people voted than what is required, time to play the game of chance.
            {
                for (int x = 0; x < GuardsToSpawn; x++)
                {
                    Player Ply = GuardPlayers[random.Next(GuardPlayers.Count)];
                    PlayersToSpawnAsGuard.Add(Ply);
                    GuardPlayers.Remove(Ply); // Removing winner from the list
                    BulkList.Remove(Ply); // Removing the winners from the bulk list
                }
                GuardsToSpawn = 0;
            }
        }

        // ---------------------------------------------------------------------------------------\\
        // SCPs
        if (SCPsToSpawn != 0)
        {
            if (SCPPlayers.Count <= SCPsToSpawn) // Less people (or equal) voted than what is required in the game.
            {
                foreach (Player ply in SCPPlayers)
                {
                    PlayersToSpawnAsSCP.Add(ply);
                    SCPsToSpawn -= 1;
                    BulkList.Remove(ply);
                }
            }
            else // More people voted than what is required, time to play the game of chance.
            {
                for (int x = 0; x < SCPsToSpawn; x++)
                {
                    Player Ply = SCPPlayers[random.Next(SCPPlayers.Count)];
                    SCPPlayers.Remove(Ply);
                    PlayersToSpawnAsSCP.Add(Ply); // Removing winner from the list
                    BulkList.Remove(Ply); // Removing the winners from the bulk list
                }
                SCPsToSpawn = 0;
            }
        }
        // ---------------------------------------------------------------------------------------\\
        // ---------------------------------------------------------------------------------------\\
        // ---------------------------------------------------------------------------------------\\
        // ---------------------------------------------------------------------------------------\\

        // At this point we need to check for any blanks and fill them in via the bulk list guys
        if (ClassDsToSpawn != 0)
        {
            for (int x = 0; x < ClassDsToSpawn; x++)
            {
                Player Ply = BulkList[random.Next(BulkList.Count)];
                PlayersToSpawnAsClassD.Add(Ply);
                BulkList.Remove(Ply); // Removing the winners from the bulk list
            }
        }
        if (SCPsToSpawn != 0)
        {
            for (int x = 0; x < SCPsToSpawn; x++)
            {
                Player Ply = BulkList[random.Next(BulkList.Count)];
                PlayersToSpawnAsSCP.Add(Ply);
                BulkList.Remove(Ply); // Removing the winners from the bulk list
            }
        }
        if (ScientistsToSpawn != 0)
        {
            for (int x = 0; x < ScientistsToSpawn; x++)
            {
                Player Ply = BulkList[random.Next(BulkList.Count)];
                PlayersToSpawnAsScientist.Add(Ply);
                BulkList.Remove(Ply); // Removing the winners from the bulk list
            }
        }
        if (GuardsToSpawn != 0)
        {
            for (int x = 0; x < GuardsToSpawn; x++)
            {
                Player Ply = BulkList[random.Next(BulkList.Count)];
                PlayersToSpawnAsGuard.Add(Ply);
                BulkList.Remove(Ply); // Removing the winners from the bulk list
            }
        }

        // ---------------------------------------------------------------------------------------\\

        // Okay we have the list! Time to spawn everyone in, we'll leave SCP for last as it has a bit of logic.
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

        // ---------------------------------------------------------------------------------------\\

        // SCP Logic, preventing SCP-079 from spawning if there isn't at least 2 other SCPs
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
        GameObject.Find("StartRound").transform.localScale = Vector3.zero;
        ObjectSpawner.SpawnSchematic("CustomSpawnerLobby", SpawnPoint, Quaternion.identity);

        if (LobbyTimer.IsRunning)
        {
            Timing.KillCoroutines(LobbyTimer);
        }
        LobbyTimer = Timing.RunCoroutine(LobbyMethods.LobbyTimer());

    }
    public void VerifiedPlayer(VerifiedEventArgs ev)
    {


        if (!ManyTweaks.Singleton.Config.GlobalVoiceChat)
        {
            MirrorExtensions.SendFakeSyncVar(ev.Player, RoundStart.singleton.netIdentity, typeof(RoundStart), "NetworkTimer", -1);
        }
        if (!Round.IsStarted && (GameCore.RoundStart.singleton.NetworkTimer > 1 || GameCore.RoundStart.singleton.NetworkTimer == -2))
        {
            Timing.CallDelayed(0.5f, () =>
            {
                if (Round.IsStarted || (GameCore.RoundStart.singleton.NetworkTimer <= 1 &&
                                        GameCore.RoundStart.singleton.NetworkTimer != -2)) return;
                ev.Player.IsOverwatchEnabled = false;
                ev.Player.Role.Type = RoleType.Tutorial;
                Scp096.TurnedPlayers.Add(ev.Player);
            });

            Timing.CallDelayed(1.5f, () =>
            {
                if (Round.IsStarted || (GameCore.RoundStart.singleton.NetworkTimer <= 1 &&
                                        GameCore.RoundStart.singleton.NetworkTimer != -2)) return;
                ev.Player.Position = SpawnPoint + Vector3.up;
                ev.Player.AddItem(ItemType.Coin);
            });


        }
    }
}
