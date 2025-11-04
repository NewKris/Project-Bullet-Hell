using System;
using NewKris.Runtime.Utility.CommonObjects;
using NewKris.Runtime.Utility.Timers;
using UnityEngine;
using UnityEngine.Pool;

namespace NewKris.Runtime.Ship.Weapons {
    public class MachineGun : Weapon {
        public GameObject bulletPrefab;
        public Transform[] bulletSpawns;
        public AudioClip[] bulletSounds;
        public float fireRate;
        
        private bool _firing;
        private int _nextSpawnIndex;
        private Timer _fireCooldown;
        private PrefabPool _bulletPool;
        private AudioSource _bulletAudioSource;
            
        public override void BeginFire() {
            _firing = true;
        }
        
        public override void EndFire() {
            _firing = false;
        }

        private void Awake() {
            Transform projectileParent = GameObject.FindGameObjectWithTag("Projectile Parent").transform;
            _bulletPool = new PrefabPool(bulletPrefab, projectileParent, 100, 50);

            _fireCooldown = TimerManager.CreateTimer();
            
            _bulletAudioSource = GetComponent<AudioSource>();
        }

        private void OnDestroy() {
            TimerManager.RemoveTimer(_fireCooldown);
        }

        private void Update() {
            if (!_firing || !_fireCooldown.Elapsed) {
                return;
            }
            
            _fireCooldown.SetTimer(fireRate);
            SpawnBullet();
        }

        private void SpawnBullet() {
            if (_bulletPool.GetObject(out GameObject bullet)) {
                bullet.transform.position = bulletSpawns[_nextSpawnIndex].position;
                bullet.gameObject.SetActive(true);
                PlayBulletSound();

                _nextSpawnIndex = (_nextSpawnIndex + 1) % bulletSpawns.Length;
            }
        }

        private void PlayBulletSound() {
            int randomSoundIndex = UnityEngine.Random.Range(0, bulletSounds.Length);
            _bulletAudioSource.PlayOneShot(bulletSounds[randomSoundIndex]);
        }
    }
}
