using System;
using NewKris.Runtime.Utility;
using NewKris.Runtime.Utility.Extensions;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    public class Missile : MonoBehaviour {
        [Header("Movement")]
        public float maxFlightSpeed;
        public float maxTurningSpeed;
        
        [Header("Detection")]
        public float detectionAngle;
        public float detectionRange;
        
        private void OnDrawGizmosSelected() {
            HandlesProxy.DrawArc(transform.position, transform.forward, Vector3.up, detectionAngle, detectionRange, 12, 3, Color.red);
        }
    }
}
