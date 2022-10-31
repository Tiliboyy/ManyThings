using Exiled.API.Features;
using System.Collections.Generic;
using UnityEngine;

namespace ManyThings.LobbySpawner
{
    public class ClassDSpawner : MonoBehaviour
    {
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerEnter(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;

            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1000, Plugin.Instance.Translation.Classdmessge);
            }

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
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerEnter(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;

            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1000, Plugin.Instance.Translation.Scpmessage);
            }
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
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerEnter(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;

            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1000, Plugin.Instance.Translation.Guardmessage);
            }
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
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerEnter(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;
            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1000, Plugin.Instance.Translation.Scientistmessage);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            var player = Player.Get(other.gameObject);
            if (player == null) return;
            player.ClearBroadcasts();
        }
        
    }
}