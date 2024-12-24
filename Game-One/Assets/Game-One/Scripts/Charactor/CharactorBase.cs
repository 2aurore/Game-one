using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ONE
{
    public class CharactorBase : MonoBehaviour
    {

        public float moveSpeed = 3.0f;


        public bool IsEquip { get; private set; }
        public bool IsRun { get; private set; }

        private Animator animator;
        private NavMeshAgent navAgent;

        private Vector2 movementInput;
        private Vector3 inputDirection;
        private float smoothHorizontal;
        private float smoothVertical;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            smoothHorizontal = Mathf.Lerp(smoothHorizontal, inputDirection.x, Time.deltaTime * 10f);
            smoothVertical = Mathf.Lerp(smoothVertical, inputDirection.z, Time.deltaTime * 10f);

            animator.SetFloat("Equip Blend", IsEquip ? 1.0f : 0.0f);
            animator.SetFloat("Run Blend", IsRun ? 1.0f : 0.0f);
            animator.SetFloat("Horizontal", smoothHorizontal);
            animator.SetFloat("Vertical", smoothVertical);
        }

        public void Move(Vector2 input, float yAxisAngle)
        {

        }

        public void SetDestination(Vector3 destination)
        {
            navAgent.SetDestination(destination);
        }
    }
}
