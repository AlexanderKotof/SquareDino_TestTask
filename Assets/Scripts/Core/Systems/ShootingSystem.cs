﻿using System;
using TestTask.Components;
using TestTask.Core;
using TestTask.Input;
using TestTask.Pooling;
using UnityEngine;
using VContainer.Unity;

namespace TestTask.GameSystems
{
    public class ShootingSystem : IInitializable, IDisposable
    {
        private const int _prespawnCount = 5;

        private readonly PlayerSpawnSystem _playerSystem;
        private readonly GameSettings _settings;
        private readonly IPlayerInputService input;
        private ObjectPool<BulletComponent> _bulletPool;
        private bool _isActive;

        public event Action OnEnemyDied;

        public ShootingSystem(PlayerSpawnSystem playerSystem, GameSettings settings, IPlayerInputService input)
        {
            _bulletPool = new ObjectPool<BulletComponent>(settings.BulletPrefab, null, _prespawnCount);

            _playerSystem = playerSystem;
            _settings = settings;
            this.input = input;
        }
        public void Initialize()
        {
            BulletComponent.HitEnemy += OnHitEnemy;
            input.ShootInput += Shoot;
        }
        public void Dispose()
        {
            BulletComponent.HitEnemy -= OnHitEnemy;
            input.ShootInput -= Shoot;
            _bulletPool.Dispose();
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        public void Shoot(Vector3 position)
        {
            if (!_isActive)
                return;

            var ray = _playerSystem.Player.playerCamera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out var hit, _settings.ShootingDistance))
            {
                var direction = hit.point - _playerSystem.Player.bulletSpawnPoint.position;

                SpawnBullet(_playerSystem.Player.bulletSpawnPoint.position, direction);
                _playerSystem.Player.Shoot(direction);
            }
        }

        private void OnHitEnemy(EnemyComponent enemy, BulletComponent bullet)
        {
            enemy.TakeDamage(_settings.BulletDamage);

            if (enemy.IsDied)
            {
                var force = (bullet.Direction + Vector3.up).normalized * _settings.RagdollForceMultiplier;
                enemy.TriggerRagdoll(force, bullet.transform.position);

                OnEnemyDied?.Invoke();
            }
        }

        private void SpawnBullet(Vector3 position, Vector3 direction)
        {
            _bulletPool.Pool().Shoot(position, direction.normalized * _settings.BulletSpeeed, _settings.BulletDamage);
        }
    }
}