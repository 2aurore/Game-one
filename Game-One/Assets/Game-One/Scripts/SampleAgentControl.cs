using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ONE
{
    public class SampleAgentControl : MonoBehaviour
    {
        public Transform TargetDestinationPoint;

        NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            agent.SetDestination(TargetDestinationPoint.position);
        }
    }
}
