using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [CreateAssetMenu(fileName = "New Charater Stat", menuName = "Game One/Charater/Create New Character Stat")]
    public class CharacterStat : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; set; } = 3.0f;
        [field: SerializeField] public float MaxHP { get; set; }
        [field: SerializeField] public float MaxSP { get; set; }

    }
}
