using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestTask.Components
{
    public class PlayerComponent : MonoBehaviour
    {
        public Camera playerCamera;
        public NavMeshAgent agent;
        public Transform bulletSpawnPoint;
        public Animator animator;

        private const float _movingVelocityThreashold = 0.1f;

        public void MoveToPosition(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void Shoot(Vector3 direction)
        {
            animator.SetTrigger("Shoot");
            // transform.rotation = Quaternion.LookRotation(direction);
        }

        private void Update()
        {
            bool isMoving = agent.velocity.sqrMagnitude > _movingVelocityThreashold;
            animator.SetBool("IsMoving", isMoving);
        }
    }
}