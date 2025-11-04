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
        private float _lastFiredTime;
        private PrefabPool _bulletPool;
        private AudioSource _audio;
        
        private float TimeSinceLastBullet => Time.time - _lastFiredTime;
            
        public override void BeginFire() {
            _firing = true;
        }
        
        public override void EndFire() {
            _firing = false;
        }

        private void Awake() {
            Transform projectileParent = GameObject.FindGameObjectWithTag("Projectile Parent").transform;
            _bulletPool = new PrefabPool(bulletPrefab, projectileParent, 100, 50);

            _audio = GetComponent<AudioSource>();
        }

        private void Update() {
            if (_firing && TimeSinceLastBullet >= fireRate) {
                SpawnBullet();
            }
        }

        private void SpawnBullet() {
            if (_bulletPool.GetObject(out GameObject bullet)) {
                bullet.transform.position = bulletSpawns[_nextSpawnIndex].position;
                bullet.gameObject.SetActive(true);
                PlayBulletSound();

                _nextSpawnIndex = (_nextSpawnIndex + 1) % bulletSpawns.Length;
            }
            
            _lastFiredTime = Time.time;
        }

        private void PlayBulletSound() {
            int randomSoundIndex = UnityEngine.Random.Range(0, bulletSounds.Length);
            _audio.PlayOneShot(bulletSounds[randomSoundIndex]);
        }
    }
}
