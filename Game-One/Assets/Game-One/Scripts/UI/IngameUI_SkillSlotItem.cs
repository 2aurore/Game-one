using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ONE
{
    public class IngameUI_SkillSlotItem : MonoBehaviour
    {
        public void SetSkillData(Sprite skill_icon, string shortcutKey)
        {
            skillIcon.sprite = skill_icon;
            shortcutText.text = shortcutKey;
        }

        public void SetCoolTime(float remain, float max)
        {
            coolTimeText.gameObject.SetActive(remain > 0f);
            coolTimeText.text = $"{remain:0}s";
            coolTimeDimd.fillAmount = remain / max;
        }

        [SerializeField] private Image skillIcon;
        [SerializeField] private TextMeshProUGUI shortcutText;
        [SerializeField] private TextMeshProUGUI coolTimeText;
        [SerializeField] private Image coolTimeDimd;

    }
}
