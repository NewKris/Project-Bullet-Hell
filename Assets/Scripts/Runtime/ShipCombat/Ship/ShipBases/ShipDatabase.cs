using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Ship.ShipBases {
    [CreateAssetMenu(menuName = "Ship/Ship Database")]
    public class ShipDatabase : ScriptableObject {
        public ShipBaseData[] ships;
    }
}
