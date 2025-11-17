using UnityEngine;
using UnityEngine.Events;

namespace Werehorse.Runtime.Hub {
    public class Interactable : MonoBehaviour {
        public UnityEvent onInteract;
        
        public void Interact() {
            onInteract.Invoke();
        }
    }
}
