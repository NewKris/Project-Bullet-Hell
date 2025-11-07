using System;
using NewKris.Runtime.Utility.CommonObjects;
using UnityEngine;

namespace NewKris.Runtime.Ship.Weapons {
    public class MissileLauncher : Weapon {
        public float fireRate;
        public GameObject missilePrefab;
        public Transform missileSpawn;
        
        private float _lastFire;
        private PrefabPool _missilePool;
        
        public override void BeginFire() {
            if (Time.time - _lastFire < fireRate) {
                return;
            }

            if (_missilePool.GetObject(out GameObject missile)) {
                missile.transform.position = missileSpawn.position;
                missile.transform.rotation = Quaternion.identity;
                missile.SetActive(true);
            }
            
            _lastFire = Time.time;
        }
        
        public override void EndFire() { }

        private void Awake() {
            Transform projectileParent = GameObject.FindGameObjectWithTag("Projectile Parent").transform;
            _missilePool = new PrefabPool(missilePrefab, projectileParent, 10, 10);
        }
    }
}
