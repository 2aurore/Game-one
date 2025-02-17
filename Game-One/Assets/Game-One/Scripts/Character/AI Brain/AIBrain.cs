using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class AIBrain : MonoBehaviour
    {

        public Transform[] patrolPoints;
        public AIController Controller => controller;
        public CharacterDetectionSensor DetectionSensor => detectionSensor;

        private AIController controller;
        private AIStateBase currentState = null;
        private CharacterDetectionSensor detectionSensor;


        private void Awake()
        {
            controller = GetComponent<AIController>();
            detectionSensor = GetComponentInChildren<CharacterDetectionSensor>();
        }

        private void Start()
        {
            ChangeState(new AIState_Patrol(this, patrolPoints));
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public void ChangeState(AIStateBase newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }


    }
}
