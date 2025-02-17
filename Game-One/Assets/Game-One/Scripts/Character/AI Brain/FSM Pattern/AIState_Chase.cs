using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class AIState_Chase : AIStateBase
    {

        private CharacterBase target;

        public AIState_Chase(AIBrain brain, CharacterBase target) : base(brain)
        {
            this.target = target;
        }

        public override void Enter()
        {
            brain.DetectionSensor.OnLostCharacter += OnLostCharacter;
        }

        private void OnLostCharacter(CharacterBase character)
        {
            if (target == character)
            {
                brain.ChangeState(new AIState_Patrol(brain, brain.patrolPoints));
            }
        }

        public override void Exit()
        {
            brain.DetectionSensor.OnLostCharacter -= OnLostCharacter;
        }

        public override void Update()
        {
            float distance = Vector3.Distance(brain.transform.position, target.transform.position);
            if (distance <= 1f)
            {
                // TODO: Attack 행위를 한다던지
            }
            else
            {
                brain.Controller.SetDestination(target.transform.position);
            }
        }


    }
}
