using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Skill_DeathFire : SkillBase
    {
        public override void ExecuteSkill(CharactorBase actor)
        {
            // TODO : DeathFire 스킬을 사용했을 때의 행동을 구현
            actor.IsProgressingAction = true;
            actor.ExecuteSkillAnimation(this.SkillData.SkillAnimationStateName);

            CurrentCoolDown = SkillCoolDown;
        }
    }
}
