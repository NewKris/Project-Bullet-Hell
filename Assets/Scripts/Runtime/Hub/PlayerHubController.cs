using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.Hub {
    public class PlayerHubController : MonoBehaviour {
        public static event Action OnInteract;
        
        public int hubActionMap = 1;

        private InputAction _lookAction;
        private InputAction _moveAction;
        
        public static Vector2 Look { get; private set; }
        public static Vector2 Move { get; private set; }
        
        private void Awake() {
            _lookAction = InputSystem.actions.actionMaps[hubActionMap]["Look"];
            _moveAction = InputSystem.actions.actionMaps[hubActionMap]["Move"];
            
            InputSystem.actions.actionMaps[hubActionMap]["Interact"].performed += _ => OnInteract?.Invoke();
            
            InputSystem.actions.actionMaps[hubActionMap].Enable();
        }

        private void OnDestroy() {
            InputSystem.actions.actionMaps[hubActionMap].Dispose();
        }

        private void Update() {
            Look = _lookAction.ReadValue<Vector2>();
            Move = _moveAction.ReadValue<Vector2>();
        }
    }
}
