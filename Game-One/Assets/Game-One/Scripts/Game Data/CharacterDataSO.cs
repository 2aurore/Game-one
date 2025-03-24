using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Game One/Character Data", order = 0)]
    public class CharacterDataSO : ScriptableObject
    {
        [field: SerializeField] public Sprite CharacterPortrait { get; set; }
        [field: SerializeField] public SerializableDictionary<string, AudioClip> Voices { get; set; }
    }
}
