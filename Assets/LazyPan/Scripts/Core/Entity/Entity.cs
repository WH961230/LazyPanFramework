using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LazyPan {
    public class Entity {
        public int GoInstanceID;
        public uint ID;
        public string GoSign;
        public Go Go;
        public int Type;
        public bool isLocalMainPlayer;
        public int Health;
        public BehaviourData BehaviourData = new BehaviourData();
        public List<Behaviour> Behaviours = new List<Behaviour>();
        public List<Entity> OwnedEntities = new List<Entity>();
        public Sprite IconSprite;

        public void Damage(int value, UnityAction callback) {
            Health -= value;
            callback?.Invoke();
        }
    }
}