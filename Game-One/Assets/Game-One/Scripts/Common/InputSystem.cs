using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class InputSystem : SingletonBase<InputSystem>
    {

        public System.Action OnRightMouseButtonDown;

        public System.Action OnEscapeInput;
        public System.Action OnTab;
        public System.Action<float> OnScrollWheel;

        public System.Action OnSpaceInput;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnRightMouseButtonDown?.Invoke();
            }

            // esc key
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnEscapeInput?.Invoke();
            }
            // tab key
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                OnTab?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpaceInput?.Invoke();
            }

            // 마우스 휠 control
            if (Input.mouseScrollDelta.y > 0)
            {
                OnScrollWheel?.Invoke(Input.mouseScrollDelta.y);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                OnScrollWheel?.Invoke(Input.mouseScrollDelta.y);
            }

        }


    }
}
