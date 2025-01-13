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
            // ���� Behaivour �� ����Ǵ� Animator�� State ���� ���� �� 1���� ����Ǵ� ����.
            if (linkedCharacter)
            {
                linkedCharacter.IsProgressingAction = false;
            }
        }
    }
}
