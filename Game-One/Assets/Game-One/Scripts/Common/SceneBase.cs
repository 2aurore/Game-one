using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public abstract class SceneBase : MonoBehaviour
    {
        public abstract IEnumerator OnStart();
        public abstract IEnumerator OnEnd();

    }
}
