using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [System.Serializable]
    public abstract class GameDataDTO { }

    [System.Serializable]
    public class SkillDataDTO : GameDataDTO
    {
        public List<SkillDataSO> SkillDatas = new List<SkillDataSO>();
    }
}
