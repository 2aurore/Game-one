using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ONE
{
    public class CharacterBase : MonoBehaviour
    {
        // void OnDrawGizmos()
        // {
        //     if (navAgent && navAgent.path.corners.Length > 0)
        //     {
        //         for (int i = 0; i < navAgent.path.corners.Length; i++)
        //         {
        //             if (i == 0)
        //             {    // 시작점
        //                 Gizmos.color = new Color(1f, 0f, 0f, 0.7f); // 빨강강
        //             }
        //             else if (i == navAgent.path.corners.Length - 1)
        //             {   // 도착점
        //                 Gizmos.color = new Color(0f, 1f, 0f, 0.7f); // 녹색색
        //             }
        //             else
        //             {   // 중간 지점들
        //                 Gizmos.color = new Color(0f, 0f, 1f, 0.7f); // 파랑
        //             }
        //             Gizmos.DrawSphere(navAgent.path.corners[i], 0.5f);
        //         }

        //         navAgent.steeringTarget => corners[1]
        //         Gizmos.color = new Color(1f, 1f, 0f, 0.7f);
        //         Gizmos.DrawCube(navAgent.steeringTarget, Vector3.one * 0.7f);

        //     }
        // }


        public bool IsEquip { get; private set; }
        public bool IsRun { get; private set; }
        public bool IsProgressingAction { get; set; }

        private Animator animator;
        private Weapon_Gun weapon_Gun;
        private NavMeshAgent navAgent;
        private Vector3 inputDirection;


        public CharacterStat defaultStat;

        public float moveSpeed = 3.0f;
        public float currentHP;
        public float maxHP;
        public float currentSP;
        public float maxSP;

        private Dictionary<KeyCode, SkillBase> skills = new Dictionary<KeyCode, SkillBase>();
        public void AddSkill(KeyCode keyCode, SkillBase skillBase)
        {
            skills.TryAdd(keyCode, skillBase);
        }
        public void RemoveSkill(KeyCode keyCode)
        {
            skills.Remove(keyCode);
        }


        private void Awake()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            weapon_Gun = GetComponent<Weapon_Gun>();

            navAgent.updatePosition = false;
            navAgent.updateRotation = false;
        }

        private void Start()
        {
            currentHP = maxHP = defaultStat.MaxHP;
            currentSP = maxSP = defaultStat.MaxSP;

            IngameUI.Instance.SetHP(currentHP, maxHP);
            IngameUI.Instance.SetSP(currentSP, maxSP);
            IngameUI.Instance.Init(skills);
        }

        private void Update()
        {
            UpdateSkills(Time.deltaTime);
            UpdateAnimationParamter();
            SynchronizeAnimatorAndAgent();

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

            animator.SetFloat("Equip Blend", IsEquip ? 1.0f : 0.0f);
            animator.SetFloat("Running Blend", IsRun ? 1.0f : 0.0f);

        }

        /// <summary>
        /// skill 재사용 대기시간 update
        /// </summary>
        private void UpdateSkills(float deltaTime)
        {
            foreach (var skill in skills.Values)
            {
                skill.UpdateSkill(deltaTime);
            }
        }

        private Vector2 velocity;
        private Vector2 smoothDeltaPosition;

        private void SynchronizeAnimatorAndAgent()
        {
            Vector3 worldDeltaPosition = navAgent.nextPosition - transform.position;
            worldDeltaPosition.y = 0f;

            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

            velocity = smoothDeltaPosition / Time.deltaTime;
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                velocity = Vector2.Lerp(Vector2.zero, velocity, navAgent.remainingDistance / navAgent.stoppingDistance);
            }

            bool shouldMove = velocity.magnitude > 0.5f && navAgent.remainingDistance > navAgent.stoppingDistance;

            if (shouldMove)
            {
                Vector3 direction = (navAgent.path.corners[1] - transform.position).normalized;
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }
            animator.SetBool("IsMoving", shouldMove);
            animator.SetFloat("Magnitude", velocity.magnitude > 0f ? velocity.magnitude : 0f);

            float deltaMagnitude = worldDeltaPosition.magnitude;
            if (deltaMagnitude > navAgent.radius * 0.5f)
            {
                transform.position = Vector3.Lerp(animator.rootPosition, navAgent.nextPosition, smooth);
            }
        }

        private void UpdateAnimationParamter()
        {
            inputDirection.z = navAgent.desiredVelocity.magnitude > 0f ? 1f : 0f;
        }

        public void SetDestination(Vector3 destination)
        {
            if (isDashing || IsProgressingAction)
                return;

            navAgent.SetDestination(destination);
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

        public void Looting()
        {
            if (isDashing || IsProgressingAction)
                return;


            animator.SetTrigger("Loot Trigger");
            navAgent.ResetPath();
        }

        /// <summary>
        /// 기본 공격 method
        /// </summary>
        public void NormalAttack(float yAxisAngle)
        {
            if (IsProgressingAction)
                return;

            IsProgressingAction = true;

            animator.Play("Defalut Attack");
            navAgent.ResetPath();
            transform.rotation = Quaternion.Euler(0f, yAxisAngle, 0f);

            weapon_Gun.Fire(yAxisAngle);
        }

        /// <summary>
        /// 스킬 공격 method
        /// </summary>
        public void SkillAttack(float yAxisAngle, KeyCode keyCode)
        {
            if (IsProgressingAction)
                return;

            skills.TryGetValue(keyCode, out SkillBase skill);

            if (skill != null)
            {
                if (skill.CurrentCoolDown <= 0)
                {
                    skill.ExecuteSkill(this);

                    navAgent.ResetPath();
                    transform.rotation = Quaternion.Euler(0f, yAxisAngle, 0f);
                }
            }
        }

        public void ExecuteSkillAnimation(string skillAnimationStateName)
        {
            animator.Play(skillAnimationStateName);
        }
    }
}
