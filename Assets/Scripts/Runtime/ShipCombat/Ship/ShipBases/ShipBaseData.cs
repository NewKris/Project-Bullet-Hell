using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Ship.ShipBases {
    [CreateAssetMenu(menuName = "Ship/Ship Base")]
    public class ShipBaseData : ScriptableObject {
        public int id;
        public GameObject shipBasePrefab;
        public Sprite icon;
    }
}
