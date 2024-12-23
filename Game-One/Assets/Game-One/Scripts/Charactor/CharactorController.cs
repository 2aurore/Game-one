using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class CharactorController : MonoBehaviour
    {
        private CharactorBase linkedCharactor;

        private void Awake()
        {
            linkedCharactor = GetComponent<CharactorBase>();
        }

        private void Start()
        {
            InputSystem.Singleton.OnRightMouseButtonDown += RightMouseButtonEvent;
        }

        private void OnDestroy()
        {
            if (InputSystem.Singleton)
            {
                InputSystem.Singleton.OnRightMouseButtonDown -= RightMouseButtonEvent;
            }
        }

        private void Update()
        {

        }

        private void RightMouseButtonEvent()
        {
            // TODO : Mouse 우클릭 이벤트 처리
            // TODO : 카메라 상에서 보았을 때, 우클릭 위치[스크린상에서의 위치] => 3D 공간에서의 위치로 변환
            // TODO : LinkedCharacter 한테 이동 명령[좌표 전달]을 한다.
            
        }
    }
}
