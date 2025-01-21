using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class AnimationEventListener : MonoBehaviour
    {
        public System.Action<string> OnReceiveAnimationEvent;

        public void ReceiveEvent(string eventKey)
        {
            OnReceiveAnimationEvent?.Invoke(eventKey);
        }
    }
}
