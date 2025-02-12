using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Skill_PerfectShot : SkillBase
    {
        public override void ExecuteSkill(CharacterBase actor)
        {
            // actor의 데이터가 필요한 경우 가져올 수 잇음

            //TODO: 스킬을 사용했을때 행동 구현
            actor.IsProgressingAction = true;
            actor.ExecuteSkillAnimation(this.SkillData.SkillAnimationStateName);

            CurrentCoolDown = SkillCoolDown;

            ApplyDamage(actor);
        }

        Vector3 attackPosition;
        private void ApplyDamage(CharacterBase actor)
        {
            if (Camera.main == null) return;

            // 마우스 방향을 구하는 Ray 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 direction = ray.direction.normalized;

            // 캐릭터의 위치와 마우스 포인터의 위치값
            Vector3 origin = actor.transform.position;

            // 마우스와 캐릭터 사이 거리 측정
            float mouseDistance = Vector3.Distance(origin, ray.origin);

            // 일정 거리 이상이면 최대 거리로 제한
            float attackDistance = Mathf.Min(mouseDistance, SkillData.Range);

            // 최종 타격 위치 계산
            attackPosition = origin + direction * attackDistance;

            LayerMask mask = LayerMask.GetMask("Monster");

            // OverlapSphere로 해당 위치의 콜라이더 검색
            Collider[] hitColliders = Physics.OverlapSphere(attackPosition, SkillData.Range / 2, mask);

            if (hitColliders.Length > 0)
            {
                foreach (Collider hit in hitColliders)
                {
                    if (hit.transform.root.TryGetComponent(out IDamage damageInterface))
                    {
                        Debug.Log($"적 명중! {hit.name} 에게 데미지 적용");
                        damageInterface.ApplyDamage(SkillData.Damage);
                    }
                }
            }

            // 디버그용 구체 시각화
            Debug.DrawRay(origin, direction * attackDistance, Color.red, 1f);
            Debug.Log($"타격 위치: {attackPosition}");
        }

        private void OnDrawGizmos()
        {
            // 디버그용 구체 시각화
            Debug.Log($"타격 위치: {attackPosition}");

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(attackPosition, SkillData.Range / 2);
        }
    }
}
