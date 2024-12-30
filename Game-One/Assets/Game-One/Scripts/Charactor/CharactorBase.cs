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
            navAgent.updateRotation = false;
        }

        private void Update()
        {
            smoothHorizontal = Mathf.Lerp(smoothHorizontal, inputDirection.x, Time.deltaTime * 10f);
            smoothVertical = Mathf.Lerp(smoothVertical, inputDirection.z, Time.deltaTime * 10f);

            animator.SetFloat("Equip Blend", IsEquip ? 1.0f : 0.0f);
            animator.SetFloat("Running Blend", IsRun ? 1.0f : 0.0f);
            animator.SetFloat("Horizontal", smoothHorizontal);
            animator.SetFloat("Vertical", smoothVertical);

            RotateToNextPoint();
        }

        public void Move(Vector2 input, float yAxisAngle)
        {

        }

        public void RotateToNextPoint()
        {
            Debug.Log("Nav Agent Path Data");
            for (int i = 0; i < navAgent.path.corners.Length; i++)
            {
                Debug.Log($"Path Corners [{i}] : {navAgent.path.corners[i]}");
            }
            Debug.Log("Nav Agent Path Data End");

            if (navAgent.path.corners.Length > 1)
            {
                Vector3 direction = (navAgent.path.corners[1] - transform.position).normalized;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }

        public void SetDestination(Vector3 destination)
        {
            navAgent.SetDestination(destination);
        }

        private void OnDrawGizmos()
        {
            if (navAgent)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.7f);

                for (int i = 0; i < navAgent.path.corners.Length; i++)
                {
                    Gizmos.DrawSphere(navAgent.path.corners[i], 0.5f);
                }

            }

        }
    }
}
