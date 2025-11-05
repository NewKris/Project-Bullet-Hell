using UnityEngine;
using UnityEngine.Events;

namespace NewKris.Runtime.Combat {
    public class TickingHitBox : MonoBehaviour {
        public int damage;
        public float tickRate;
        public Faction canHitFaction;
        public UnityEvent onHit;

        private float _lastTick;
        
        private bool TickElapsed => Time.time - _lastTick > tickRate;
        
        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("Hit Box");
        }

        private void OnTriggerStay(Collider other) {
            if (TickElapsed && other.TryGetComponent(out HurtBox hurtBox) && CanHurtFaction(hurtBox.isFaction)) {
                hurtBox.TakeDamage(damage);
                onHit.Invoke();
                _lastTick = Time.time;
            }
        }
        
        private bool CanHurtFaction(Faction faction) {
            return (canHitFaction & faction) != 0;
        }
    }
}
