using System;
using NewKris.Runtime.Combat;
using NewKris.Runtime.Utility;
using NewKris.Runtime.Utility.Extensions;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    public class Missile : MonoBehaviour {
        public int explosionDamage;

        [Header("Movement")] 
        public float accelerationSpeed;
        public float maxFlightSpeed;
        public float maxTurningSpeed;
        
        [Header("Detection")]
        public LayerMask detectFaction;
        public float detectionAngle;
        public float detectionRange;

        private float _speed;
        private HurtBox _target;
        private Collider[] _inRangeColliders = new Collider[10];

        public void Explode(Vector3 hitPoint) {
            ExplosionSystem.SpawnExplosion(
                hitPoint, 
                GetComponentInChildren<HitBox>().canHitFaction,
                explosionDamage
            );
            
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            _speed = 0;
            _target = null;
        }

        private void Update() {
            if (_target) {
                
            }
            
            _speed += accelerationSpeed * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, 0, maxFlightSpeed);
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }

        private GameObject FindTarget() {
            int inRangeTargets = Physics.OverlapSphereNonAlloc(
                transform.position, 
                detectionRange, 
                _inRangeColliders, 
                LayerMask.NameToLayer("Hurt Box")
            );

            return null;
        }

        private void OnDrawGizmosSelected() {
            HandlesProxy.DrawArc(
                transform.position, 
                transform.forward, 
                Vector3.up, 
                detectionAngle, 
                detectionRange, 
                12, 
                3, 
                Color.red
            );
        }
    }
}
