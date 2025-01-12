using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ONE
{
    public class IngameUI : UIBase
    {
        public static IngameUI Instance => UIManager.Singleton.GetUI<IngameUI>(UIList.IngameUI);

        public Image hpBar;
        public Image spBar;

        public TextMeshProUGUI hpText;
        public TextMeshProUGUI spText;

        public List<TextMeshProUGUI> skillCooldown;

        public void SetHP(float current, float max)
        {
            hpBar.fillAmount = current / max;
            hpText.text = $"{current:0} / {max: 0}";
        }

        public void SetSP(float current, float max)
        {
            spBar.fillAmount = current / max;
            spText.text = $"{current:0} / {max: 0}";
        }

        public void SetSkillCooldown(KeyCode keyCode, float cooldown)
        {
            switch (keyCode)
            {
                case KeyCode.Q:
                    skillCooldown[0].text = $"{cooldown}s";
                    break;
                case KeyCode.W:
                    skillCooldown[1].text = $"{cooldown}s";
                    break;
                case KeyCode.E:
                    skillCooldown[2].text = $"{cooldown}s";
                    break;
                case KeyCode.R:
                    skillCooldown[3].text = $"{cooldown}s";
                    break;
                case KeyCode.A:
                    skillCooldown[4].text = $"{cooldown}s";
                    break;
                case KeyCode.S:
                    skillCooldown[5].text = $"{cooldown}s";
                    break;
                case KeyCode.D:
                    skillCooldown[6].text = $"{cooldown}s";
                    break;
                case KeyCode.F:
                    skillCooldown[7].text = $"{cooldown}s";
                    break;


            }
        }
    }
}
