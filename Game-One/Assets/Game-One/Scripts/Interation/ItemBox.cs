using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class ItemBox : MonoBehaviour, IBox
    {
        public string itemName;
        public string Name => itemName;

        public SphereEffect sphereEffect;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Open()
        {
            LogUI.Instance.AddLogMessage("info", "Open Box!");

            sphereEffect.StartCreateSpheres();

            animator.SetTrigger("Open Trigger");
            Invoke(nameof(EndOpenEffect), 3f);
        }

        public void EndOpenEffect()
        {
            sphereEffect.EndCreateSpheres();
        }
    }
}
