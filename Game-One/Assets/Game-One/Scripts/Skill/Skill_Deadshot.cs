using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Skill_Deadshot : SkillBase
    {
        public override void ExecuteSkill(CharacterBase actor)
        {
            // actor의 데이터가 필요한 경우 가져올 수 잇음

            //TODO: 스킬을 사용했을때 행동 구현
            actor.IsProgressingAction = true;
            actor.ExecuteSkillAnimation(this.SkillData.SkillAnimationStateName);

            CurrentCoolDown = SkillCoolDown;
        }
    }
}
