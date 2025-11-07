using System;
using NewKris.Runtime.Utility.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace NewKris.Runtime.Combat {
    public class HitBox : MonoBehaviour {
        public int damage;
        public LayerMask canHitFaction;
        public UnityEvent<Vector3> onHit;

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out HurtBox hurtBox) && CanHurtFaction(hurtBox.gameObject.layer)) {
                hurtBox.TakeDamage(damage);
                onHit.Invoke(other.ClosestPoint(transform.position));
            }
        }

        private bool CanHurtFaction(LayerMask faction) {
            return canHitFaction.ContainstLayer(faction);
        }
    }
}
