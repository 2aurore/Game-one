using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [CreateAssetMenu(fileName = "New Skill Stat", menuName = "Game One/Skill/Create New Skill Stat")]
    public class SkillDataSO : ScriptableObject
    {
        [field: SerializeField] public string Skill_ID { get; set; }
        [field: SerializeField] public string SkillAnimationStateName { get; set; }

        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public float Range { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public float Damage { get; set; }



    }
}
