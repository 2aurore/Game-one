using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [CreateAssetMenu(fileName = "New Character Stat", menuName = "Game One/Character/Create New Character Stat")]
    public class CharacterStat : ScriptableObject
    {
        [field: Header("Character Stat")]
        [field: SerializeField] public float MoveSpeed { get; set; } = 3.0f;
        [field: SerializeField] public float MaxHP { get; set; }
        [field: SerializeField] public float MaxSP { get; set; }
    }
}
