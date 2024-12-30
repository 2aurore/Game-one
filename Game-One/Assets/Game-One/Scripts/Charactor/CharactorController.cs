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
            InputSystem.Singleton.OnRightMouseButtonDown += RightMouseButtonEvent;
            InputSystem.Singleton.OnSpaceInput += SpaceInputEvent;
        }

        private void OnDestroy()
        {
            if (InputSystem.Singleton)
            {
                InputSystem.Singleton.OnRightMouseButtonDown -= RightMouseButtonEvent;
                InputSystem.Singleton.OnSpaceInput -= SpaceInputEvent;
            }
        }

        private void Update()
        {

        }

        private void RightMouseButtonEvent()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, groundLayer, QueryTriggerInteraction.Ignore))
            {
                // Ray 가 Ground Layer에 부딪힌 경우.
                linkedCharactor.SetDestination(hitInfo.point);
                lastRightClickPoint = hitInfo.point;
            }
        }

        private void SpaceInputEvent()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000f, groundLayer, QueryTriggerInteraction.Ignore))
            {                
                Vector3 direction = (hitInfo.point - linkedCharactor.transform.position).normalized;
                float yAxisAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                linkedCharactor.Dash(yAxisAngle);
            }
        }

        private Vector3 lastRightClickPoint;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.7f);
            Gizmos.DrawSphere(lastRightClickPoint, 0.5f);
        }
    }
}
