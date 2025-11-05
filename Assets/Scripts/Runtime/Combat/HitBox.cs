using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewKris.Runtime.Combat {
    public class HitBox : MonoBehaviour {
        public int damage;
        public Faction canHitFaction;
        public UnityEvent onHit;
        
        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("Hit Box");
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out HurtBox hurtBox) && CanHurtFaction(hurtBox.isFaction)) {
                hurtBox.TakeDamage(damage);
                onHit.Invoke();
            }
        }

        private bool CanHurtFaction(Faction faction) {
            return (canHitFaction & faction) != 0;
        }
    }
}
