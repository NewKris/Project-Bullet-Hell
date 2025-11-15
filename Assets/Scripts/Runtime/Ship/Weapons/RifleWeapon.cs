using System;
using UnityEngine;
using Werehorse.Runtime.Combat.Projectiles.SimpleProjectiles;

namespace Werehorse.Runtime.Ship.Weapons {
    public class RifleWeapon : Weapon {
        public float fireRate;
        public float rayRadius;
        public float maxRayDistance;
        public float minRayDistance;
        public LayerMask lockOnTargets;

        private bool _firing;
        private float _lastFireTime;
        
        private bool CanFire => Time.time > _lastFireTime + fireRate;
        
        public override void BeginFire() {
            _firing = true;
        }

        public override void EndFire() {
            _firing = false;
        }

        private void Update() {
            if (!_firing || !CanFire) {
                return;
            }
            
            _lastFireTime = Time.time;
            
            if (SimpleProjectileSystem.GetProjectile(out GameObject projectile)) {
                projectile.transform.position = transform.position;

                Vector3 dir = GetTarget() - projectile.transform.position;
                projectile.transform.rotation = Quaternion.LookRotation(dir);
                projectile.SetActive(true);
            }
        }

        private Vector3 GetTarget() {
            Ray ray = Camera.main.ScreenPointToRay(PlayerController.MousePosition);
            
            bool hitTarget = Physics.SphereCast(ray, rayRadius, out RaycastHit hit, maxRayDistance, lockOnTargets);
            float distance = hitTarget && hit.distance > minRayDistance ? hit.distance : maxRayDistance;
            
            return ray.GetPoint(distance);
        }
    }
}
