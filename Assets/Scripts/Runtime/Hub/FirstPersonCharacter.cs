using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Hub {
    public class FirstPersonCharacter : MonoBehaviour {
        public Transform playerCamera;

        [Header("Movement")] 
        public float maxMoveSpeed;
        public CharacterController characterController;
        
        [Header("Camera")]
        public Vector3 cameraOffset;
        public float sensitivity;
        public float yawScale;
        public float pitchScale;
        public float minPitch;
        public float maxPitch;
        public float lookDamping;

        private DampedAngle _yaw;
        private DampedAngle _pitch;
        private Transform _yawPivot;
        private Transform _pitchPivot;

        private void OnValidate() {
            if (playerCamera) {
                playerCamera.position = transform.TransformPoint(cameraOffset);
            }
        }

        private void Start() {
            SetupCamera();
        }

        private void Update() {
            Look(Time.deltaTime);
            Move();
        }

        private void Move() {
            Vector3 velocity = PlayerHubController.Move.ProjectOnGround();
            velocity = _yawPivot.localRotation * velocity;
            
            characterController.SimpleMove(velocity * maxMoveSpeed);
        }

        private void Look(float dt) {
            float deltaYaw = PlayerHubController.Look.x * yawScale * sensitivity;
            _yaw.Target += deltaYaw;
            _yaw.Target %= 360;
            _yawPivot.localRotation = Quaternion.Euler(0, _yaw.Tick(lookDamping, dt), 0);
            
            float deltaPitch = PlayerHubController.Look.y * pitchScale * sensitivity;
            _pitch.Target -= deltaPitch;
            _pitch.Target = Mathf.Clamp(_pitch.Target, minPitch, maxPitch);
            _pitchPivot.localRotation = Quaternion.Euler(_pitch.Tick(lookDamping, dt), 0, 0);
        }
        
        private void SetupCamera() {
            _yawPivot = new GameObject("Yaw Pivot").transform;
            _pitchPivot = new GameObject("Pitch Pivot").transform;
            
            _yawPivot.SetParent(transform);
            _yawPivot.SetLocalPositionAndRotation(cameraOffset, Quaternion.identity);
            
            _pitchPivot.SetParent(_yawPivot);
            _pitchPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            playerCamera.SetParent(_pitchPivot);
            playerCamera.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            _yaw = new DampedAngle();
            _pitch = new DampedAngle();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
