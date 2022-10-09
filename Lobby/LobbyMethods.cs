using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using Respawning.NamingRules;
using Respawning;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using System.Security.Cryptography;
using UnityEngine;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ManyThings
{
    public static class LobbyMethods
    {
        internal static IEnumerator<float> LobbyTimer()
        {
            while (!Round.IsStarted)
            {
                string text = string.Empty;

                if (Plugin.Instance.Config.HintVertPos != 0 && Plugin.Instance.Config.HintVertPos < 0)
                {
                    for (int i = Plugin.Instance.Config.HintVertPos; i < 0; i++)
                    {
                        text += "\n";
                    }
                }

                text += Plugin.Instance.Translation.TopMessage;

                text += $"\n{Plugin.Instance.Translation.BottomMessage}";

                short networkTimer = GameCore.RoundStart.singleton.NetworkTimer;

                switch (networkTimer)
                {
                    case -2: text = text.Replace("{seconds}", Plugin.Instance.Translation.ServerIsPaused); break;

                    case -1: text = text.Replace("{seconds}", Plugin.Instance.Translation.RoundIsBeingStarted); break;

                    case 1: text = text.Replace("{seconds}", $"{networkTimer} {Plugin.Instance.Translation.OneSecondRemain}"); break;

                    case 0: text = text.Replace("{seconds}", Plugin.Instance.Translation.RoundIsBeingStarted); break;

                    default: text = text.Replace("{seconds}", $"{networkTimer} {Plugin.Instance.Translation.XSecondsRemains}"); break;
                }

                if (Player.List.Count() == 1)
                {
                    text = text.Replace("{players}", $"{Player.List.Count()} {Plugin.Instance.Translation.OnePlayerConnected}");
                }
                else
                {
                    text = text.Replace("{players}", $"{Player.List.Count()} {Plugin.Instance.Translation.XPlayersConnected}");
                }

                if (Plugin.Instance.Config.HintVertPos != 0 && Plugin.Instance.Config.HintVertPos > 0)
                {
                    for (int i = 0; i < Plugin.Instance.Config.HintVertPos; i++)
                    {
                        text += "\n";
                    }
                }

                foreach (Player player in Player.List)
                {
                    if (Plugin.Instance.Config.UseHints)
                    {
                        player.ShowHint(text.ToString(), 1.1f);
                    }
                    else
                    {
                        player.Broadcast(1, text.ToString());
                    }
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }

        public static void SendFakeUnitName(Player target, string name, SpawnableTeamType spawnableTeamType = SpawnableTeamType.NineTailedFox)
        {
            Log.Debug($"{nameof(SendFakeUnitName)}: Sending {target.Nickname} a fake unit name: {name}", Plugin.Instance.Config.IsDebug);
            MirrorExtensions.SendFakeSyncObject(target, RespawnManager.Singleton.NamingManager.netIdentity, typeof(UnitNamingManager), writer =>
            {
                writer.WriteUInt64(1ul);
                writer.WriteUInt32(1);
                writer.WriteByte((byte)SyncList<SyncUnit>.Operation.OP_ADD);
                writer.WriteByte((byte)spawnableTeamType);
                writer.WriteString(name);
            });
            target.SendFakeSyncVar(Server.Host.ReferenceHub.networkIdentity, typeof(CharacterClassManager), nameof(CharacterClassManager.NetworkCurClass), (sbyte)RoleType.NtfCaptain);
            target.UnitName = target.Role.ToString();
            target.SendFakeSyncVar(Server.Host.ReferenceHub.networkIdentity, typeof(CharacterClassManager), nameof(CharacterClassManager.NetworkCurClass), (sbyte)RoleType.NtfCaptain);
        }
        
        public static IEnumerator<float> LobbyTimer2()
        {
            StringBuilder message = new StringBuilder();
            int x = 0;

            while (!Round.IsStarted)   
            {
                message.Clear();


                message.Append($"<size=40><color=yellow><b>{Plugin.Instance.Translation.RoundIsBeingStarted}, %seconds</b></color></size>");

                short NetworkTimer = GameCore.RoundStart.singleton.NetworkTimer;

                switch (NetworkTimer)
                {
                    case -2: message.Replace("%seconds", Plugin.Instance.Translation.ServerIsPaused); break;

                    case -1: message.Replace("%seconds", Plugin.Instance.Translation.RoundIsBeingStarted); break;

                    case 1: message.Replace("%seconds", $"{NetworkTimer} {Plugin.Instance.Translation.XSecondsRemains}"); break;

                    case 0: message.Replace("%seconds", Plugin.Instance.Translation.RoundIsBeingStarted); break;

                    default: message.Replace("%seconds", $"{NetworkTimer} {Plugin.Instance.Translation.XSecondsRemains}"); break;
                }

                message.Append($"\n<size=30><i>%players</i></size>");

                if (Player.List.Count() == 1) message.Replace("%players", $"{Player.List.Count()} {Plugin.Instance.Translation.OnePlayerConnected}");
                else message.Replace("%players", $"{Player.List.Count()} {Plugin.Instance.Translation.XPlayersConnected}");

                Vector3 SpawnPoint = Plugin.Instance.Config.SpawnPoint;
                for (int i = 0; i < Plugin.Instance.Config.HintVertPos; i++)
                {
                    Log.Info(i);
                    message.Append("\n");
                }
                foreach (Player ply in Player.List)
                {
                    ply.ShowHint(message.ToString(), 1f);

                    if (Vector3.Distance(ply.Position, Plugin.Instance.Config.ScpSpawner + SpawnPoint) <= 3.7)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Scpmessage}</i>");
                    }
                    else if (Vector3.Distance(ply.Position, Plugin.Instance.Config.ClassDSpawner + SpawnPoint) <= 3.7)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Classdmessge}</i>");
                    }
                    else if (Vector3.Distance(ply.Position, Plugin.Instance.Config.ScientistSpawner + SpawnPoint) <= 3.7)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Scientistmessage}</i>");
                    }
                    else if (Vector3.Distance(ply.Position, Plugin.Instance.Config.GuardSpawner + SpawnPoint) <= 3.7)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Guardmessage}</i>");
                    }
                    else
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Randommessage}</i>");
                    }
                }
                x++;
                yield return Timing.WaitForSeconds(1f);
            }
        }

    }
}
