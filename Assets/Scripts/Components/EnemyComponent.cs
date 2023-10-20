using System.Linq;
using UnityEngine;

namespace TestTask.Components
{
    public class EnemyComponent : MonoBehaviour
    {
        public int startHealth = 2;
        private int health;

        public bool IsDied => health <= 0;

        public Collider hitCollider;

        public HealthbarComponent healthbar;

        public Animator animator;

        public Rigidbody[] ragdollRigidbodies;

        private const float _dynamicTime = 3f;

        private void Start()
        {
            health = startHealth;

            SwitchRagdoll(false);
        }

        public void ShowHealthbar(PlayerComponent player)
        {
            healthbar.Initialize(player.playerCamera);
            healthbar.UpdateHealth(startHealth, health);
        }

        public void TakeDamage(int dmg)
        {
            health -= dmg;

            healthbar.UpdateHealth(startHealth, health);
        }

        public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
        {
            SwitchRagdoll(true);

            foreach (var rb in ragdollRigidbodies)
            {
                var hitRigidbodyDistance = Vector3.SqrMagnitude(rb.position - hitPoint);

                force = Vector3.ClampMagnitude(force / (1 + hitRigidbodyDistance), 1000);

                rb.AddForceAtPosition(force, hitPoint);
            }

            Invoke(nameof(SetKinematic), _dynamicTime);
        }

        public void SetKinematic()
        {
            foreach (var rigidbody in ragdollRigidbodies)
            {
                rigidbody.isKinematic = true;
            }
        }

        private void SwitchRagdoll(bool enableRagdoll)
        {
            animator.enabled = !enableRagdoll;

            foreach (var rigidbody in ragdollRigidbodies)
            {
                rigidbody.isKinematic = !enableRagdoll;
            }

            hitCollider.enabled = !enableRagdoll;
        }
    }
}