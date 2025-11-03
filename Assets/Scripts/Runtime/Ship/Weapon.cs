using UnityEngine;

namespace NewKris.Runtime.Ship {
    public abstract class Weapon : MonoBehaviour {
        public abstract void BeginFire();
        public abstract void EndFire();
    }
}
