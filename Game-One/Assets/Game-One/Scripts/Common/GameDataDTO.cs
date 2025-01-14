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


}
