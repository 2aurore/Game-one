using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CharactorController : MonoBehaviour
    {
        private CharactorBase linkedCharactor;

        public LayerMask groundLayer;

        private void Awake()
        {
            linkedCharactor = GetComponent<CharactorBase>();
        }

        private void Start()
        {
            InputSystem.Singleton.OnLeftMouseButtonDown += LeftMouseButtonEvent;
            InputSystem.Singleton.OnRightMouseButtonDown += RightMouseButtonEvent;
            InputSystem.Singleton.OnSpaceInput += SpaceInputEvent;
        }


        private void OnDestroy()
        {
            if (InputSystem.Singleton)
            {
                InputSystem.Singleton.OnLeftMouseButtonDown -= LeftMouseButtonEvent;
                InputSystem.Singleton.OnRightMouseButtonDown -= RightMouseButtonEvent;
                InputSystem.Singleton.OnSpaceInput -= SpaceInputEvent;
            }
        }


        private void Update()
        {

        }

        private void SpaceInputEvent()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000F, groundLayer, QueryTriggerInteraction.Ignore))
            {
                Vector3 direction = (hitInfo.point - linkedCharactor.transform.position).normalized;
                float yAxisAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                linkedCharactor.Dash(yAxisAngle);
            }

        }

        private void LeftMouseButtonEvent()
        {
            // TODO : Mouse 좌클릭 이벤트 처리
            // 마우스 포인터 방향으로 공격액션 수행하도록 적용
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000F, groundLayer, QueryTriggerInteraction.Ignore))
            {
                Vector3 direction = (hitInfo.point - linkedCharactor.transform.position).normalized;
                float yAxisAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                linkedCharactor.NormalAttack(yAxisAngle);
            }
        }

        private void RightMouseButtonEvent()
        {
            // TODO : Mouse 우클릭 이벤트 처리
            // TODO : 카메라 상에서 보았을 때, 우클릭 위치[스크린상에서의 위치] => 3D 공간에서의 위치로 변환
            // TODO : LinkedCharacter 한테 이동 명령[좌표 전달]을 한다.

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, groundLayer, QueryTriggerInteraction.Ignore))
            {
                // Ray 가 Ground Layer에 부딫힌 경우
                linkedCharactor.SetDestination(hitInfo.point);
                lastRightClickPoint = hitInfo.point;
            }
        }

        private Vector3 lastRightClickPoint;
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            Gizmos.DrawSphere(lastRightClickPoint, 1f);
        }
    }
}
