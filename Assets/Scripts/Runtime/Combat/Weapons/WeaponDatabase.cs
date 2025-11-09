using UnityEngine;

namespace NewKris.Runtime.Combat.Weapons {
    [CreateAssetMenu(menuName = "Weapon Database")]
    public class WeaponDatabase : ScriptableObject {
        public WeaponData[] weapons;
    }
}
