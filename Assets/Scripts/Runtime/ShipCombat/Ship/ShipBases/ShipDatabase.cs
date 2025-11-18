using System.Linq;
using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Ship.ShipBases {
    [CreateAssetMenu(menuName = "Ship/Ship Database")]
    public class ShipDatabase : ScriptableObject {
        public ShipBaseData[] ships;

        public ShipBaseData GetShipData(int id) {
            return ships.First(data => data.id == id);
        }
    }
}
