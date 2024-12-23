using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CharactorController : MonoBehaviour
    {
        public CharactorBase linkedCharactor;

        private void Awake()
        {
            linkedCharactor = GetComponent<CharactorBase>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            float mouseX = Input.GetAxis("Mouse X");
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            // T 키가 눌렸을때 상태를 전환
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (!linkedCharactor.IsAttack)
                {
                    linkedCharactor.Pose();
                }
            }
            // 마우스 좌클릭 시 공격 모션 
            if (Input.GetMouseButtonDown(0) && !linkedCharactor.IsPosing)
            {
                // 이미 공격 모션 중일 때 새롭게 공격모션을 취할 수 없도록 적용
                if (!linkedCharactor.IsAttack)
                {
                    linkedCharactor.Attack();
                }
            }

            if (!linkedCharactor.IsAttack)
            {
                Vector2 input = new Vector2(horizontal, vertical);
                linkedCharactor.Move(input);
                linkedCharactor.Rotate(mouseX);
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                // UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby Scene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            }

        }
    }
}
