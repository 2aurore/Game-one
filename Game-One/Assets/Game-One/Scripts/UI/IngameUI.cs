using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class IngameUI : UIBase
    {
        public static IngameUI Instance => UIManager.Singleton.GetUI<IngameUI>(UIList.IngameUI);

        public void SetHP(float current, float max)
        {
            // hpBar.fillAmount = current / max;
            // hpText.text = $"{current:0} / {max: 0}";
        }

        public void SetSP(float current, float max)
        {
            // spBar.fillAmount = current / max;
            // spText.text = $"{current:0} / {max: 0}";
        }
    }
}
