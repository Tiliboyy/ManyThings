using Exiled.API.Features;
using System.Collections.Generic;
using UnityEngine;

namespace LobbySpawner
{
    public class ClassDSpawner : MonoBehaviour
    {
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerStay(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;

            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1, ManyTweaks.Singleton.Config.Classdmessge);
            }
        }
    }
    public class ScpSpawner : MonoBehaviour
    {
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerStay(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;

            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1, ManyTweaks.Singleton.Config.Scpmessage);
            }
        }
    }
    public class GuardSpawner : MonoBehaviour
    {
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerStay(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;

            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1, ManyTweaks.Singleton.Config.Guardmessage);
            }
        }
    }
    public class ScientistSpawner : MonoBehaviour
    {
        public Dictionary<GameObject, float> timeGameObject = new Dictionary<GameObject, float>();
        public void OnTriggerStay(Collider other)
        {
            if (!timeGameObject.ContainsKey(other.gameObject)) timeGameObject[other.gameObject] = -1;
            if (Time.time > timeGameObject[other.gameObject])
            {
                timeGameObject[other.gameObject] = Time.time + 1;
                var player = Player.Get(other.gameObject);
                if (player == null) return;
                player = Player.Get(other.gameObject);
                player.ClearBroadcasts();
                player.Broadcast(1, ManyTweaks.Singleton.Config.ScientistMessage);
            }
        }
    }
}
