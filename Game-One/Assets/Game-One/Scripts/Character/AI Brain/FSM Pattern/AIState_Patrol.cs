using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class AIState_Patrol : AIStateBase
    {
        private Transform[] patrolPoints;
        private int currentPatrolIndex = 0;

        public AIState_Patrol(AIBrain brain, Transform[] patrolPoints) : base(brain)
        {
            this.patrolPoints = patrolPoints;
        }

        public override void Enter()
        {
            currentPatrolIndex = 0;
            brain.Controller.SetDestination(patrolPoints[currentPatrolIndex].position);
            brain.Controller.OnDestinationReached += OnDestinationReached;

            brain.DetectionSensor.OnDetectedCharacter += OnDetectedCharacter;
        }

        public override void Exit()
        {
            brain.DetectionSensor.OnDetectedCharacter -= OnDetectedCharacter;

        }

        public override void Update()
        {
        }

        private void OnDetectedCharacter(CharacterBase character)
        {
            brain.ChangeState(new AIState_Chase(brain, character));
        }

        private void OnDestinationReached()
        {
            brain.StartCoroutine(WaitToNextPatrolPoint());
            IEnumerator WaitToNextPatrolPoint()
            {
                yield return new WaitForSeconds(3f);

                currentPatrolIndex++;
                if (currentPatrolIndex >= patrolPoints.Length)
                    currentPatrolIndex = 0;

                brain.Controller.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }

    }
}
