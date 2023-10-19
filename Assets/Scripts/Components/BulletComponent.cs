using System;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public new Rigidbody rigidbody;

    public float lifeTime = 5f;

    public Vector3 Direction => _velocity.normalized;

    private float _spawnTime;
    private Vector3 _velocity;

    public static event Action<EnemyComponent, BulletComponent> HitEnemy;

    public void Shoot(Vector3 position, Vector3 velocity, int damage)
    {
        gameObject.SetActive(true);
        _spawnTime = Time.realtimeSinceStartup;

        _velocity = velocity;

        transform.position = position;
        transform.rotation = Quaternion.LookRotation(_velocity);
    }

    private void Update()
    {
        rigidbody.velocity = _velocity;

        if (_spawnTime + lifeTime < Time.realtimeSinceStartup)
            Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyComponent>(out var enemyComponent))
        {
            HitEnemy?.Invoke(enemyComponent, this);
        }

        Despawn();
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
    }
}
