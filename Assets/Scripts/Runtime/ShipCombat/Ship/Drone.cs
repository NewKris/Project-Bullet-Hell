using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;

namespace Werehorse.Runtime.ShipCombat.Ship {
    public class Drone : MonoBehaviour {
        public Transform target;
        public float followDamping;
        public float rotateDamping;

        private bool _initialized;
        private DampedRotation _rotation;
        private DampedVector _position;

        public void SetTarget(Transform newTarget, bool snapToTarget = false) {
            target = newTarget;

            if (snapToTarget) {
                _position = new DampedVector(target.position);
                _rotation = new DampedRotation(target.rotation);         
            }
            else {
                _position = new DampedVector(transform.position);
                _rotation = new DampedRotation(transform.rotation);
            }

            _initialized = true;
        }

        private void Start() {
            if (target && !_initialized) {
                SetTarget(target);
            }
        }

        private void Update() {
            _position.Target = target.position;
            transform.position = _position.Tick(followDamping);
            
            _rotation.Target = target.rotation;
            transform.rotation = _rotation.Tick(rotateDamping);
        }
    }
}
