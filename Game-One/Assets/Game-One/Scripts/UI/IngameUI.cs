using System;
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


        private readonly KeyCode[] keyCodes =
        {
            KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
            KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F
        };


        public SkillDataDTO SkillDataDTO { get; private set; } = new SkillDataDTO();

        public Dictionary<KeyCode, GameObject> skillSlots_backup = new Dictionary<KeyCode, GameObject>();
        public Dictionary<KeyCode, SkillBase> skillDatas; // 스킬 데이터 관리
        public SerializableDictionary<KeyCode, IngameUI_SkillSlotItem> skillSlots = new SerializableDictionary<KeyCode, IngameUI_SkillSlotItem>();  // 슬롯 관리


        public void Init(Dictionary<KeyCode, SkillBase> skills)
        {
            skillDatas = skills;

            InitializeSkillData();
        }

        private void InitializeSkillData()
        {
            foreach (var keycode in keyCodes)
            {
                if (skillDatas.TryGetValue(keycode, out var skillDataSO))
                {
                    if (skillSlots.TryGetValue(keycode, out var skillSlotSO))
                    {
                        skillSlotSO.SetSkillData(skillDataSO.SkillData.Icon, keycode.ToString());
                    }
                }
            }
        }

        public void Update()
        {
            foreach (var keycode in keyCodes)
            {
                if (skillDatas.TryGetValue(keycode, out var skillDataSO))
                {
                    if (skillSlots.TryGetValue(keycode, out var skillSlotSO))
                    {
                        skillSlotSO.SetCoolTime(skillDataSO.CurrentCoolDown, skillDataSO.SkillCoolDown);
                    }
                }
            }
        }

    }
}
