using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class SkillStateMachineBehavier : StateMachineBehaviour
    {

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent(out CharacterBase character))
            {
                character.IsProgressingAction = false;
            }
        }
    }
}
