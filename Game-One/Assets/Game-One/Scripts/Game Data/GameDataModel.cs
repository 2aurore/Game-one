using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class GameDataModel : SingletonBase<GameDataModel>
    {
        [field: SerializeField]
        public SkillDataDTO SkillDataDTO { get; private set; } = new SkillDataDTO();

        public CharacterDataDto CharacterDataDto { get; private set; } = new CharacterDataDto();

        public void Initalize()
        {
            // game data parsing
            SkillDataSO[] loadedDatas = Resources.LoadAll<SkillDataSO>("Game Data/Skill Data/");



            for (int i = 0; i < loadedDatas.Length; i++)
            {
                string skill_id = loadedDatas[i].Skill_ID;
                if (!string.IsNullOrEmpty(skill_id))
                {
                    if (!SkillDataDTO.skillDatas.ContainsKey(skill_id))
                    {
                        SkillDataDTO.skillDatas.Add(skill_id, loadedDatas[i]);
                    }
                    else
                    {
                        Debug.LogWarning($"Duplicate skill name detected: {skill_id}. Skipping.");
                    }
                }
                else
                {
                    Debug.LogWarning($"SkillDataSO with missing skill name detected: {loadedDatas[i]}. Skipping.");
                }
            }
        }

        public bool GetSkillData(string skill_id, out SkillDataSO result)
        {
            result = SkillDataDTO.skillDatas.GetValueOrDefault(skill_id, null);
            return result != null;
        }
    }
}
