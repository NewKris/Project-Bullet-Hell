using System;
using UnityEngine;

namespace NewKris.Runtime.Combat {
    public class EntityFlash : MonoBehaviour {
        private static readonly int FlashStrength = Shader.PropertyToID("_Flash_Strength");

        public float flashDecreaseSpeed;
        public MeshRenderer meshRenderer;

        private float _flashStrength;
        private MaterialPropertyBlock _materialPropertyBlock;
        
        public void Flash() {
            _flashStrength = 1;
        }

        private void Awake() {
            _materialPropertyBlock = new MaterialPropertyBlock();
            _materialPropertyBlock.SetFloat(FlashStrength, 0);
            meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void Update() {
            _flashStrength -= flashDecreaseSpeed * Time.deltaTime;
            _flashStrength = Mathf.Clamp01(_flashStrength);
            
            _materialPropertyBlock.SetFloat(FlashStrength, _flashStrength);
            meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}
