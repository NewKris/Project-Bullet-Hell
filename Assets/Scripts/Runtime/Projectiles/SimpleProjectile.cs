using NewKris.Runtime.Combat;
using NewKris.Runtime.Ship.Weapons;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    public class SimpleProjectile : MonoBehaviour {
        public float maxSpeed;
        public Vector3 direction;

        public void Hit() {
            gameObject.SetActive(false);
        }
    }
}
