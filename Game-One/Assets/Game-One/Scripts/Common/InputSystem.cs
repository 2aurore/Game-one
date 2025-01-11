using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class InputSystem : SingletonBase<InputSystem>
    {

        public System.Action OnLeftMouseButtonDown;
        public System.Action OnRightMouseButtonDown;

        public System.Action OnEscapeInput;
        public System.Action OnTab;
        public System.Action<float> OnScrollWheel;
        public System.Action OnSpaceInput;

        public System.Action<KeyCode> OnKeyInput;


        private void Update()
        {
            // 마우스 버튼 입력 처리
            if (Input.GetMouseButtonDown(0)) OnLeftMouseButtonDown?.Invoke();
            if (Input.GetMouseButtonDown(1)) OnRightMouseButtonDown?.Invoke();

            // 키보드 키 입력 처리
            if (Input.GetKeyUp(KeyCode.Escape)) OnEscapeInput?.Invoke();
            if (Input.GetKeyUp(KeyCode.Tab)) OnTab?.Invoke();
            if (Input.GetKeyDown(KeyCode.Space)) OnSpaceInput?.Invoke();

            // 마우스 휠 스크롤 처리
            if (Input.mouseScrollDelta.y != 0) OnScrollWheel?.Invoke(Input.mouseScrollDelta.y);

            // QWERASDF 키 입력 처리
            CheckKeyInput(KeyCode.Q);
            CheckKeyInput(KeyCode.W);
            CheckKeyInput(KeyCode.E);
            CheckKeyInput(KeyCode.R);
            CheckKeyInput(KeyCode.A);
            CheckKeyInput(KeyCode.S);
            CheckKeyInput(KeyCode.D);
            CheckKeyInput(KeyCode.F);
        }

        private void CheckKeyInput(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                OnKeyInput?.Invoke(key);
            }
        }
    }

}
