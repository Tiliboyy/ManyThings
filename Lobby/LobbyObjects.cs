using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace ManyThings.LobbySpawner
{
    public class ClassDSpawner : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {

            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player = Player.Get(other.gameObject);
            player.Broadcast(60, Plugin.Instance.Translation.Classdmessge, Broadcast.BroadcastFlags.Normal, true);

        }
        public void OnTriggerExit(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player.ClearBroadcasts();
        }
    }
    public class ScpSpawner : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player = Player.Get(other.gameObject);
            player.Broadcast(60, Plugin.Instance.Translation.Scpmessage, Broadcast.BroadcastFlags.Normal, true);

        }
        public void OnTriggerExit(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player.ClearBroadcasts();
        }
    }
    public class GuardSpawner : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player = Player.Get(other.gameObject);
            player.Broadcast(60, Plugin.Instance.Translation.Guardmessage, Broadcast.BroadcastFlags.Normal, true);
        }

        public void OnTriggerExit(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player.ClearBroadcasts();
        }
    }
    public class ScientistSpawner : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player = Player.Get(other.gameObject);
            player.Broadcast(60, Plugin.Instance.Translation.Scientistmessage, Broadcast.BroadcastFlags.Normal, true);
        }
        public void OnTriggerExit(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player.ClearBroadcasts();
        }
        
    }
}