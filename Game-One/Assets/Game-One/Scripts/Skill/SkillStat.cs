using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ONE
{
    [CreateAssetMenu(fileName = "New Skill Stat", menuName = "Game One/Skill/Create New Skill Stat")]
    public class SkillStat : ScriptableObject
    {
        [field: SerializeField] public string SkillName { get; set; }
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public float Range { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public float Damage { get; set; }
        private float lastUsedTime = -Mathf.Infinity; // 마지막 사용 시간 기록


        // 스킬이 재사용 가능한지 확인
        public bool IsReady()
        {
            return Time.time >= lastUsedTime + Cooldown;
        }

        // 스킬 사용
        public bool Use()
        {
            if (IsReady())
            {
                lastUsedTime = Time.time; // 현재 시간으로 갱신
                Debug.Log($"{SkillName} used! Cooldown: {Cooldown} seconds.");
                return true;
            }
            else
            {
                Debug.Log($"{SkillName} is on cooldown. Time left: {Mathf.Max(0, (lastUsedTime + Cooldown) - Time.time)} seconds.");
                return false;
            }
        }

        // 남은 쿨다운 시간 반환
        public float GetRemainingCooldown()
        {
            return Mathf.Max(0, (lastUsedTime + Cooldown) - Time.time);
        }
    }
}
