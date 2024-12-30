using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ONE
{
    public class FollowerCharacter : MonoBehaviour
    {
        public Transform followTarget;
        public Vector3 offset;

        private NavMeshAgent navAgent;

        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (followTarget != null)
            {
                Vector3 targetPosition = followTarget.position + offset;
                navAgent.SetDestination(targetPosition);
            }
        }
    }
}
