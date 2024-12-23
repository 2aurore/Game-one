using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CharactorBase : MonoBehaviour
    {
        public bool IsPosing { get; set; }
        public bool IsAttack { get; set; }
        public bool IsAlive => currentHP > 0f;


        public float moveSpeed = 2f;
        public Animator animator;

        public float currentHP;
        public float maxHP;
        public float currentSP;
        public float maxSP;

        private bool isValidRunning;
        private float animationParameterSpeed;
        private float animationParameterHorizontal;
        private float animationParameterVertical;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentHP = maxHP;
            currentSP = maxSP;

            IngameUI.Instance.SetHP(currentHP, maxHP);
            IngameUI.Instance.SetSP(currentSP, maxSP);

            IsAttack = false;
        }

        private void Update()
        {
            if (!IsAlive) return;

            currentSP = Mathf.Clamp(currentSP, 0f, maxSP); // SP 값이 0보다 작아지지 않고 max값 보다 커지지 않기
            IngameUI.Instance.SetSP(currentSP, maxSP);

            animator.SetFloat("Speed", animationParameterSpeed);
            animator.SetFloat("Horizontal", animationParameterHorizontal);
            animator.SetFloat("Vertical", animationParameterVertical);
            animator.SetBool("Posing", IsPosing);
        }


        public void Move(Vector2 input)
        {
            if (!IsAlive) return;

            animationParameterSpeed = input.sqrMagnitude > 0f ? 0.5f : 0f;
            animationParameterHorizontal = input.x;
            animationParameterVertical = input.y;

            Vector3 movement = new Vector3(input.x, 0f, input.y);
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
        }

        public void Rotate(float yAxis)
        {
            if (!IsAlive) return;

            // 캐릭터가 포즈 중일 때는 마우스 위치에 따라 회전하지 않도록 함
            if (!IsPosing)
            {
                transform.Rotate(Vector3.up * yAxis);
            }
        }

        public void Pose()
        {
            animator.SetTrigger("Pose Trigger");
            IsPosing = !IsPosing;
        }

        public void Attack()
        {
            animator.SetTrigger("Attack Trigger");
            IsAttack = true;
        }

        /// <summary>
        ///  이 메서드는 애니메이션 Attack 모션 종료 시 이벤트로 호출된다.
        /// </summary>
        public void AttackEnd()
        {
            IsAttack = false;
        }
    }
}
