using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace ManyTweaks
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
    }
}
