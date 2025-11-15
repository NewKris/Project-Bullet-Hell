using System;
using UnityEngine;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.Combat.Projectiles.SimpleProjectiles {
    public class SimpleProjectile : MonoBehaviour {
        public static event Action<SimpleProjectile> OnSpawned;
        
        public float travelSpeed;
        public float lifeTime;
        [ReadOnly] public float spawnTime;

        private void OnEnable() {
            spawnTime = Time.time;
            OnSpawned?.Invoke(this);
        }
    }
}
