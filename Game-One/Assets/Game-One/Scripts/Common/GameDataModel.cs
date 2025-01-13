using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class GameDataModel : SingletonBase<GameDataModel>
    {
        [field: SerializeField] public SkillDataDTO SkillDataDTO { get; private set; } = new SkillDataDTO();


        public void Initialize()
        {
            // Game Data Parsing
            SkillDataSO[] loadedDatas = Resources.LoadAll<SkillDataSO>("Game Data/Skill Data/");
            for (int i = 0; i < loadedDatas.Length; i++)
            {
                SkillDataDTO.SkillDatas.Add(loadedDatas[i]);
            }
        }


        public bool GetSkillData(string skill_id, out SkillDataSO result)
        {
            result = SkillDataDTO.SkillDatas.Find(x => x.Skill_ID.Equals(skill_id));
            return result != null;
        }
    }
}
