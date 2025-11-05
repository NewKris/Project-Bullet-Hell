using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewKris.Runtime.Combat {
    public class HurtBox : MonoBehaviour {
        public int maxHealth;
        public Faction isFaction;
        public UnityEvent onHurt;
        public UnityEvent onDeath;

        [Header("Contact Damage")] 
        public bool canTakeContactDamage = true;
        public int contactDamage;
        public Faction canBumpFaction;
        
        private int _health;

        public void TakeDamage(int damage) {
            _health -= damage;
            onHurt.Invoke();

            if (_health <= 0) {
                onDeath.Invoke();
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.TryGetComponent(out HurtBox hurtBox) 
                && CanHurtFaction(hurtBox.isFaction) 
                && hurtBox.canTakeContactDamage
            ) {
                hurtBox.TakeDamage(contactDamage);
            }
        }

        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("Hurt Box");
        }

        private void OnEnable() {
            _health = maxHealth;
        }
        
        private bool CanHurtFaction(Faction faction) {
            return (canBumpFaction & faction) != 0;
        }
    }
}
