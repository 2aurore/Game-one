using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class SkillStateMachineBehaviour : StateMachineBehaviour
    {
        private CharactorBase linkedCharacter;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (animator.TryGetComponent(out CharactorBase character))
            {
                linkedCharacter = character;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 현재 Behaivour 가 실행되는 Animator의 State 에서 나올 때 1번만 실행되는 구역.
            if (linkedCharacter)
            {
                linkedCharacter.IsProgressingAction = false;
            }
        }
    }
}
