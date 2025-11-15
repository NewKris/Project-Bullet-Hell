using System.Collections.Generic;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Combat.Projectiles.SimpleProjectiles {
    public class SimpleProjectileSystem : MonoBehaviour {
        private static SimpleProjectileSystem Instance;
        private static HashSet<SimpleProjectile> ActiveProjectiles = new HashSet<SimpleProjectile>(50);

        public GameObject projectilePrefab;
        
        private PrefabPool _pool;

        public static bool GetProjectile(out GameObject projectile) {
            return Instance._pool.GetObject(out projectile);
        }
        
        private void Awake() {
            Instance = this;
            ActiveProjectiles.Clear();
            _pool = new PrefabPool(projectilePrefab, transform);
            SimpleProjectile.OnSpawned += RegisterProjectile;
        }

        private void OnDestroy() {
            SimpleProjectile.OnSpawned -= RegisterProjectile;
        }

        private void Update() {
            ActiveProjectiles.ForEach(MoveProjectile);
            ActiveProjectiles.ForEach(ExpireProjectile);
            ActiveProjectiles.RemoveWhere(IsInactive);
        }

        private bool IsInactive(SimpleProjectile projectile) {
            return !projectile.gameObject.activeSelf;
        }

        private void MoveProjectile(SimpleProjectile projectile) {
            Vector3 velocity = projectile.transform.forward * projectile.travelSpeed * Time.deltaTime;
            projectile.transform.position += velocity;
        }

        private void ExpireProjectile(SimpleProjectile projectile) {
            if (Time.time > projectile.spawnTime + projectile.lifeTime) {
                projectile.gameObject.SetActive(false);
            }
        }
        
        private void RegisterProjectile(SimpleProjectile projectile) {
            ActiveProjectiles.Add(projectile);
        }
    }
}
