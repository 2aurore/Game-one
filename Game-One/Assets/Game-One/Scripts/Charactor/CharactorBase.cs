using System;
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
            RotateToNextPoint();
            UpdateAnimationParamter();

            if (isDashing)
            {
                if (Time.time - dashStartTime < dashDuration)
                {
                    transform.position += transform.forward * dashSpeed * Time.deltaTime;
                }
                else
                {
                    isDashing = false;
                    animator.SetBool("IsDashing", false);
                }
            }

            smoothHorizontal = Mathf.Lerp(smoothHorizontal, inputDirection.x, Time.deltaTime * 10f);
            smoothVertical = Mathf.Lerp(smoothVertical, inputDirection.z, Time.deltaTime * 10f);

            animator.SetFloat("Equip Blend", IsEquip ? 1.0f : 0.0f);
            animator.SetFloat("Running Blend", IsRun ? 1.0f : 0.0f);
            animator.SetFloat("Horizontal", smoothHorizontal);
            animator.SetFloat("Vertical", smoothVertical);
            animator.SetFloat("Magnitude", navAgent.desiredVelocity.magnitude);

        }

        private void UpdateAnimationParamter()
        {
            inputDirection.z = navAgent.desiredVelocity.magnitude > 0f ? 1f : 0f;
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
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }
        }

        public void SetDestination(Vector3 destination)
        {
            if (isDashing)
                return;

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

        public float dashDuration = 1.5f;
        public float dashSpeed = 2f;
        private bool isDashing = false;
        private float dashStartTime = 0f;
        public void Dash(float yAxisAngle)
        {
            if (isDashing)
                return;

            isDashing = true;
            dashStartTime = Time.time;
            animator.SetBool("IsDashing", true);
            animator.SetTrigger("Dash Trigger");

            // navAgent.SetDestination(transform.position);
            navAgent.ResetPath();

            transform.rotation = Quaternion.Euler(0f, yAxisAngle, 0f);


        }
    }
}
