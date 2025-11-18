using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Werehorse.Runtime.ShipCombat.Ship.ShipBases;
using Werehorse.Runtime.ShipCombat.Ship.ShipBehaviour;
using Werehorse.Runtime.ShipCombat.Ship.Weapons;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.ShipCombat.Ship.Equipment {
    public class ShipBuilder : MonoBehaviour {
        public Transform spawnPoint;
        public Drone shipCamera;
        public RectTransform reticle;
        public ShipDatabase shipDatabase;
        public WeaponDatabase weaponDatabase;

        [Header("Overrides")] [InspectorButton(nameof(ClearEquipmentCache), "Clear Cache")] 
        public EquipmentBlackBoard defaultEquipments;

        private void Awake() {
            EquipmentBlackBoard equipment = EquipmentBlackBoard.HasEquipment ? EquipmentBlackBoard.CurrentEquipment : defaultEquipments;
            
            GameObject instance = SpawnBaseShip(equipment.shipBaseId);

            shipCamera.SetTarget(instance.transform, true);
        }

        private GameObject SpawnBaseShip(int id) {
            GameObject shipPrefab = shipDatabase.ships.First(ship => ship.id == id).shipBasePrefab;
            GameObject instance = Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation, transform);
            instance.GetComponent<PlaneShip>().reticle = reticle;
            
            return instance;
        }

        private void ClearEquipmentCache() {
            Debug.Log("Cleared equipment cache");
            EquipmentBlackBoard.SetCurrentEquipment(null);
        }
    }
}
