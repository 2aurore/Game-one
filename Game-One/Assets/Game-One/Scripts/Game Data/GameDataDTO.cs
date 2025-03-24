using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [System.Serializable]
    public class GameDataDTO { }

    [System.Serializable]
    public class SkillDataDTO : GameDataDTO
    {
        public Dictionary<string, SkillDataSO> skillDatas = new Dictionary<string, SkillDataSO>();
    }

    [System.Serializable]
    public class CharacterDataDto : GameDataDTO
    {

        [System.Serializable]
        public class CharacterData
        {
            public string id;
            public string characterName;
            public string characterDescription;
            public int characterLevel;
            public int maxHP;
            public int moveSpeed;
            public int attackSpeed;
            public int attackRange;

            public CharacterDataSO GetCharacterDataSO()
            {
                // return AssetBundle.LoadAsset<CharacterDataSO>($"GameData/CharacterDataSO/characterSO_{id}");
                return Resources.Load<CharacterDataSO>($"GameData/CharacterDataSO/characterSO_{id}");
            }
        }

        public List<CharacterData> Datas = new List<CharacterData>();
    }
}
