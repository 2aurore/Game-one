using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public abstract class AIStateBase
    {
        protected AIBrain brain;

        public AIStateBase(AIBrain brain)
        {
            this.brain = brain;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
