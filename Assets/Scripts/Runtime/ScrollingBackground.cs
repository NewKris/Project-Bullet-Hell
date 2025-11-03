using System;
using Unity.VisualScripting;
using UnityEngine;

namespace NewKris.Runtime {
    public class ScrollingBackground : MonoBehaviour {
        public float scrollSpeed;
        public float spacing;
        public float flipThreshold;
        public Vector3 scrollDirection;
        public Vector3 backgroundNormal;
        public Sprite backgroundSprite;

        private Transform _child1;
        private Transform _child2;
        
        private void Start() {
            _child1 = new GameObject().transform;
            _child1.parent = transform;
            _child1.SetLocalPositionAndRotation(Vector3.zero, Quaternion.LookRotation(backgroundNormal));
            _child1.localScale = Vector3.one;
            _child1.AddComponent<SpriteRenderer>().sprite = backgroundSprite;
            
            _child2 = new GameObject().transform;
            _child2.parent = transform;
            _child2.SetLocalPositionAndRotation(SiblingOffset(), Quaternion.LookRotation(backgroundNormal));
            _child2.localScale = Vector3.one;
            _child2.AddComponent<SpriteRenderer>().sprite = backgroundSprite;
        }

        private void Update() {
            Vector3 vel = scrollDirection.normalized * (scrollSpeed * Time.deltaTime);
            _child1.localPosition += vel;
            _child2.localPosition += vel;

            if (_child1.localPosition.sqrMagnitude > flipThreshold * flipThreshold) {
                _child1.localPosition = _child2.localPosition + SiblingOffset();
            }
            
            if (_child2.localPosition.sqrMagnitude > flipThreshold * flipThreshold) {
                _child2.localPosition = _child1.localPosition + SiblingOffset();
            }
        }

        private Vector3 SiblingOffset() {
            return -scrollDirection.normalized * spacing;
        }
    }
}