using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Skill_DeathFire : SkillBase
    {
        public override void ExecuteSkill(CharactorBase actor)
        {
            // TODO : DeathFire ��ų�� ������� ���� �ൿ�� ����
            actor.IsProgressingAction = true;
            actor.ExecuteSkillAnimation(this.SkillData.SkillAnimationStateName);

            CurrentCoolDown = SkillCoolDown;
        }
    }
}
