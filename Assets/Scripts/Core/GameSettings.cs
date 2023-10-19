using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private PlayerComponent _playerPrefab;
    [SerializeField] private BulletComponent _bulletPrefab;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private float _ragdollForceMultiplier = 500;

    public PlayerComponent PlayerPrefab => _playerPrefab;
    public BulletComponent BulletPrefab => _bulletPrefab;
    public float ShootingDistance => _shootingDistance;
    public float RagdollForceMultiplier => _ragdollForceMultiplier;
}
