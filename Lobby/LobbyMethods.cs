using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using Respawning.NamingRules;
using Respawning;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using System.Text;
using Exiled.Events.Handlers;
using Player = Exiled.API.Features.Player;

namespace ManyThings
{
    public static class LobbyMethods
    {
        public static IEnumerator<float> LobbyTimer()
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
                    message.Append("\n");
                }
                foreach (Player ply in Player.List)
                {
                    ply.ShowHint(message.ToString(), 1f);

                    if (Vector3.Distance(ply.Position, Plugin.Instance.Config.ScpSpawner + SpawnPoint) <= Plugin.Instance.Config.SpawnPadSize)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Scpmessage}</i>");
                        Log.Debug($"SCP: {ply}", Plugin.Instance.Config.IsDebug);

                    }
                    else if (Vector3.Distance(ply.Position, Plugin.Instance.Config.ClassDSpawner + SpawnPoint) <= Plugin.Instance.Config.SpawnPadSize)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Classdmessge}</i>");
                        Log.Debug($"ClassD: {ply}", Plugin.Instance.Config.IsDebug);

                    }
                    else if (Vector3.Distance(ply.Position, Plugin.Instance.Config.ScientistSpawner + SpawnPoint) <= Plugin.Instance.Config.SpawnPadSize)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Scientistmessage}</i>");
                        Log.Debug($"Scientist: {ply}", Plugin.Instance.Config.IsDebug);

                    }
                    else if (Vector3.Distance(ply.Position, Plugin.Instance.Config.GuardSpawner + SpawnPoint) <= Plugin.Instance.Config.SpawnPadSize)
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Guardmessage}</i>");
                        Log.Debug($"Guard: {ply}", Plugin.Instance.Config.IsDebug);

                    }
                    else
                    {
                        ply.Broadcast(1, $"<i>{Plugin.Instance.Translation.Randommessage}</i>");
                        Log.Debug($"Random: {ply}", Plugin.Instance.Config.IsDebug);

                    }
                }
                x++;
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
