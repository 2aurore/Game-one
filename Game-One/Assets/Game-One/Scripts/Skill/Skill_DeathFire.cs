using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Skill_DeathFire : SkillBase
    {

        public override void ExecuteSkill(CharacterBase actor)
        {
            // actor의 데이터가 필요한 경우 가져올 수 잇음

            //TODO: 스킬을 사용했을때 행동 구현
            actor.IsProgressingAction = true;
            actor.ExecuteSkillAnimation(SkillData.SkillAnimationStateName);

            CurrentCoolDown = SkillCoolDown;

            actor.StartCoroutine(RepeatApplayDamage(actor, 3, 0.3f));
        }

        private IEnumerator RepeatApplayDamage(CharacterBase actor, int repeatCount, float interval)
        {
            for (int i = 0; i < repeatCount; i++)
            {
                ApplyDamage(actor);
                yield return new WaitForSeconds(interval);
            }
        }

        private void ApplyDamage(CharacterBase actor)
        {
            LayerMask mask = LayerMask.GetMask("Monster");
            RaycastHit[] raycastHits = Physics.SphereCastAll(
                actor.transform.position, SkillData.Range, Vector3.up, 0, mask);

            // 거리에 따라 배열을 정렬
            System.Array.Sort(raycastHits, (x, y) => x.distance.CompareTo(y.distance));

            if (raycastHits.Length > 0)
            {
                foreach (RaycastHit hit in raycastHits)
                {
                    // Debug.Log(hit);
                    if (hit.collider.transform.root.TryGetComponent(out IDamage damageInterface))
                    {
                        damageInterface.ApplyDamage(SkillData.Damage);
                    }
                }
            }
        }
    }
}
