using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [System.Serializable]
    public abstract class SkillBase
    {
        public float SkillCoolDown => SkillData.Cooldown;
        public float CurrentCoolDown { get; protected set; }

        public SkillDataSO SkillData { get; private set; }


        public void Init(SkillDataSO skillData)
        {
            SkillData = skillData;
        }

        public void UpdateSkill(float deltaTime)
        {
            CurrentCoolDown -= deltaTime;
            CurrentCoolDown = Mathf.Max(0, CurrentCoolDown);
        }


        public abstract void ExecuteSkill(CharacterBase actor);
    }
}
