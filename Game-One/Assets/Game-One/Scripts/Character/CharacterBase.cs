using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ONE
{
    public class CharacterBase : MonoBehaviour
    {

        public bool IsEquip { get; private set; }
        public bool IsRun { get; private set; }
        public bool IsProgressingAction { get; set; }
        public bool IsLooting { get; private set; }

        public Transform firePointLeft;
        public Transform firePointRight;

        private Animator animator;
        private Weapon_Gun weapon_Gun;
        private NavMeshAgent navAgent;
        private AnimationEventListener animationEventListener;

        private Vector3 inputDirection;


        public CharacterStat defaultStat;

        public float moveSpeed = 3.0f;
        public float currentHP;
        public float maxHP;
        public float currentSP;
        public float maxSP;

        // dash variable
        public float dashDuration = 1.5f;
        public float dashSpeed = 2f;
        private bool isDashing = false;
        private float dashStartTime = 0f;

        public float shotDistance = 50f;

        private Vector3 lootingPosition;
        private Quaternion lootingRotation;

        public Dictionary<KeyCode, SkillBase> skills = new Dictionary<KeyCode, SkillBase>();
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
            animationEventListener = GetComponent<AnimationEventListener>();
            animationEventListener.OnReceiveAnimationEvent += OnReceiveAnimationEvent;

            navAgent.updatePosition = false;
            navAgent.updateRotation = false;
        }


        public void Initialize()
        {
            currentHP = maxHP = defaultStat.MaxHP;
            currentSP = maxSP = defaultStat.MaxSP;
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
                    Teleport(transform.position + (transform.forward * dashSpeed * Time.deltaTime));
                }
                else
                {
                    isDashing = false;
                    animator.SetBool("IsDashing", false);
                }
            }

            animator.SetFloat("Equip Blend", IsEquip ? 1.0f : 0.0f);
            animator.SetFloat("Running Blend", IsRun ? 1.0f : 0.0f);
            UpdateLootingState();
        }

        private void UpdateLootingState()
        {
            if (IsLooting)
            {
                // lootingPosition 위치로 캐릭터가 스르륵 움직이도록 처리리
                Vector3 blendPosition = Vector3.Lerp(transform.position, lootingPosition, Time.deltaTime * 10f);
                Quaternion blendRotation = Quaternion.Slerp(transform.rotation, lootingRotation, Time.deltaTime * 10f);

                Teleport(blendPosition, blendRotation);
            }
        }

        /// <summary> skill 재사용 대기시간 update </summary>
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
            // NavMeshAgent의 다음 위치와 현재 위치 사이의 차이를 계산
            Vector3 worldDeltaPosition = navAgent.nextPosition - transform.position;
            // y축(높이) 값은 무시하여 평면상의 이동만 고려
            worldDeltaPosition.y = 0f;

            // 월드 좌표의 차이를 캐릭터의 로컬 좌표계로 변환 (우측 및 전방 기준)
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            // 부드러운 이동을 위한 보간값 계산 (0.15초 기준으로 정규화)
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            // 현재 위치와 목표 위치 사이를 부드럽게 보간
            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);
            // 실제 이동 속도 계산
            velocity = smoothDeltaPosition / Time.deltaTime;

            // 목적지에 가까워질수록 속도를 감소시킴
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                velocity = Vector2.Lerp(Vector2.zero, velocity, navAgent.remainingDistance / navAgent.stoppingDistance);
            }

            // 속도와 남은 거리를 기준으로 캐릭터가 이동해야 하는지 결정
            bool shouldMove = velocity.magnitude > 0.5f && navAgent.remainingDistance > navAgent.stoppingDistance;

            // 이동해야 한다면 다음 경로 지점을 향해 캐릭터를 회전
            if (shouldMove)
            {
                // 다음 경로 지점을 향하는 방향 벡터 계산
                Vector3 direction = (navAgent.path.corners[1] - transform.position).normalized;
                // y축(높이) 값은 무시
                direction.y = 0;
                // 부드러운 회전을 위해 Slerp 사용
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }

            // 애니메이터에 이동 상태 전달
            animator.SetBool("IsMoving", shouldMove);
            // 애니메이터에 이동 속도의 크기 전달
            animator.SetFloat("Magnitude", velocity.magnitude > 0f ? velocity.magnitude : 0f);

            // NavMeshAgent와 실제 시각적 위치 사이의 불일치 해결
            float deltaMagnitude = worldDeltaPosition.magnitude;
            // 위치 차이가 일정 수준 이상이면 위치를 보간하여 조정
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

        public void Looting(Vector3 position, Quaternion rotation)
        {
            if (isDashing || IsProgressingAction)
                return;

            lootingPosition = position;
            lootingRotation = rotation;

            IsLooting = true;
            animator.SetTrigger("Loot Trigger");
            navAgent.ResetPath();
        }


        /// <summary> 총구에 발사 이펙트 적용 </summary>
        private void OnReceiveAnimationEvent(string eventKey)
        {
            switch (eventKey)
            {
                case "Fire Left":
                    {
                        GameObject newMuzzleEffect = EffectManager.Instance.GetEffect("GoldFire_Muzzle");
                        newMuzzleEffect.transform.SetParent(firePointLeft);
                        newMuzzleEffect.transform.SetPositionAndRotation(firePointLeft.position, firePointLeft.rotation);
                    }
                    break;
                case "Fire Right":
                    {
                        GameObject newMuzzleEffect = EffectManager.Instance.GetEffect("GoldFire_Muzzle");
                        newMuzzleEffect.transform.SetParent(firePointRight);
                        newMuzzleEffect.transform.SetPositionAndRotation(firePointRight.position, firePointRight.rotation);
                    }
                    break;

            }
        }

        /// <summary> 기본 공격 method </summary>
        public void NormalAttack(float yAxisAngle)
        {
            if (IsProgressingAction)
                return;

            IsProgressingAction = true;

            animator.Play("Defalut Attack");
            navAgent.ResetPath();
            transform.rotation = Quaternion.Euler(0f, yAxisAngle, 0f);

            OnReceiveAnimationEvent("Fire Left");

            Ray ray = new Ray(transform.position, transform.forward);
            LayerMask mask = LayerMask.GetMask("Monster");
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, shotDistance, mask);

            // 거리에 따라 배열을 정렬
            System.Array.Sort(raycastHits, (x, y) => x.distance.CompareTo(y.distance));

            if (raycastHits.Length > 0)
            {
                foreach (RaycastHit hit in raycastHits)
                {
                    if (hit.collider.transform.root.TryGetComponent(out IDamage damageInterface))
                    {
                        damageInterface.ApplyDamage(10f);
                    }
                }
            }
        }

        /// <summary> 스킬 공격 method </summary>
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

        public void SetLootingComplete()
        {
            IsLooting = false;
        }

        public void Teleport(Vector3 position)
        {
            transform.position = position;
            navAgent.Warp(position);
        }
        public void Teleport(Vector3 position, Quaternion rotation)
        {
            // transform.position = position;
            // transform.rotation = rotation;
            // 위의 두개 대신 SetPositionAndRotation 메소드를 사용할때 성능 부분에서 향상시킬 수 있음
            // 하위 모든 position과 rotation을 변경하기 때문에 메소드를 활용해서 1회만 업데이트 시킬수 잇도록 함

            transform.SetPositionAndRotation(position, rotation);
            navAgent.Warp(position);
        }

    }
}
