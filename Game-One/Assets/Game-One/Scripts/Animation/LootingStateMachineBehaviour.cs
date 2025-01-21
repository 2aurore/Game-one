using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class LootingStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent(out CharacterBase actor))
            {
                actor.SetLootingComplete();
            }
        }
    }
}
