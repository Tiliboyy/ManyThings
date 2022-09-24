using Exiled.API.Features;
using MapEditorReborn.Configs;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyTweaksLobby
{
    internal static class LobbyMethods
    {
        internal static IEnumerator<float> LobbyTimer()
        {
            while (!Round.IsStarted)
            {
                string text = string.Empty;

                if (ManyTweaks.Singleton.Config.HintVertPos != 0 && ManyTweaks.Singleton.Config.HintVertPos < 0)
                {
                    for (int i = ManyTweaks.Singleton.Config.HintVertPos; i < 0; i++)
                    {
                        text += "\n";
                    }
                }

                text += ManyTweaks.Singleton.Config.TopMessage;

                text += $"\n{ManyTweaks.Singleton.Config.BottomMessage}";

                short networkTimer = GameCore.RoundStart.singleton.NetworkTimer;

                switch (networkTimer)
                {
                    case -2: text = text.Replace("{seconds}", ManyTweaks.Singleton.Config.ServerIsPaused); break;

                    case -1: text = text.Replace("{seconds}", ManyTweaks.Singleton.Config.RoundIsBeingStarted); break;

                    case 1: text = text.Replace("{seconds}", $"{networkTimer} {ManyTweaks.Singleton.Config.OneSecondRemain}"); break;

                    case 0: text = text.Replace("{seconds}", ManyTweaks.Singleton.Config.RoundIsBeingStarted); break;

                    default: text = text.Replace("{seconds}", $"{networkTimer} {ManyTweaks.Singleton.Config.XSecondsRemains}"); break;
                }

                if (Player.List.Count() == 1)
                {
                    text = text.Replace("{players}", $"{Player.List.Count()} {ManyTweaks.Singleton.Config.OnePlayerConnected}");
                }
                else
                {
                    text = text.Replace("{players}", $"{Player.List.Count()} {ManyTweaks.Singleton.Config.XPlayersConnected}");
                }

                if (ManyTweaks.Singleton.Config.HintVertPos != 0 && ManyTweaks.Singleton.Config.HintVertPos > 0)
                {
                    for (int i = 0; i < ManyTweaks.Singleton.Config.HintVertPos; i++)
                    {
                        text += "\n";
                    }
                }

                foreach (Player player in Player.List)
                {
                    if (ManyTweaks.Singleton.Config.UseHints)
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
    }
}
