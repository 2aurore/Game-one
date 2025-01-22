using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Skill_PhantomStrike : SkillBase
    {

        public override void ExecuteSkill(CharacterBase actor)
        {
            // actor의 데이터가 필요한 경우 가져올 수 잇음

            //TODO: 스킬을 사용했을때 행동 구현
            actor.IsProgressingAction = true;
            actor.ExecuteSkillAnimation(this.SkillData.SkillAnimationStateName);

            CurrentCoolDown = SkillCoolDown;

            actor.StartCoroutine(RepeatApplayDamage(actor, 3, 0.1f));
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
            // "Monster" 레이어만 검출
            LayerMask mask = LayerMask.GetMask("Monster");

            // SkillData.Range 범위 내의 모든 collider 검출
            RaycastHit[] raycastHits = Physics.SphereCastAll(
                actor.transform.position,
                SkillData.Range,
                Vector3.forward,
                10f,
                mask
            );

            // 검출된 collider를 거리 순으로 정렬
            System.Array.Sort(raycastHits, (x, y) => x.distance.CompareTo(y.distance));

            // 시야각 범위 (45도는 SkillData.Range * 0.5로 정의)
            float halfViewAngle = 45f;

            // 검출된 collider 처리
            foreach (RaycastHit hit in raycastHits)
            {
                Transform hitTransform = hit.collider.transform;

                // 거리 내 방향 계산
                Vector3 directionToTarget = (hitTransform.position - actor.transform.position).normalized;

                // 시야각 조건 확인
                if (Vector3.Angle(actor.transform.forward, directionToTarget) <= halfViewAngle)
                {
                    // 대상이 IDamage 인터페이스를 구현하고 있는지 확인
                    if (hitTransform.root.TryGetComponent<IDamage>(out var damageInterface))
                    {
                        damageInterface.ApplyDamage(SkillData.Damage);
                        // Debug.Log($"Damage applied to: {hitTransform.name}");
                    }
                }
            }
        }


    }
}
