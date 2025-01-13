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

        public Dictionary<KeyCode, GameObject> skillSlots = new Dictionary<KeyCode, GameObject>(); // 슬롯 관리
        public Dictionary<KeyCode, SkillStat> skillDatas = new Dictionary<KeyCode, SkillStat>(); // 데이터 관리

        [SerializeField] private List<GameObject> slotObjects; // 슬롯 리스트
        [SerializeField] private List<SkillStat> dataObjects; // 데이터 리스트


        private void Start()
        {
            InitializeDictionaries();
            UpdateAllSkillSlots();
        }

        private void InitializeDictionaries()
        {

            // KeyCode와 슬롯 매핑
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (i < slotObjects.Count)
                {
                    skillSlots[keyCodes[i]] = slotObjects[i];
                }
            }

            // KeyCode와 데이터 매핑
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (i < dataObjects.Count)
                {
                    skillDatas[keyCodes[i]] = dataObjects[i];
                }
            }
        }
        public void UpdateAllSkillSlots()
        {
            foreach (var key in skillSlots.Keys)
            {
                if (skillDatas.ContainsKey(key))
                {
                    UpdateSkillSlot(key);
                }
            }
        }

        public void UpdateSkillSlot(KeyCode key)
        {
            // if (skillSlots.TryGetValue(key, out SkillSlot slot) && skillDatas.TryGetValue(key, out SkillData data))
            // {
            //     slot.icon.sprite = data.skillIcon;      // 아이콘 업데이트
            //     slot.cooldown.text = data.cooldown;  // 스킬 이름 업데이트
            // }
        }

    }
}
