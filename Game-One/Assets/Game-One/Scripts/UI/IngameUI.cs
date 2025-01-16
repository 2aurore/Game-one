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

        public Dictionary<KeyCode, GameObject> skillSlots = new Dictionary<KeyCode, GameObject>(); // 슬롯 관리
        public Dictionary<KeyCode, SkillBase> skillDatas; // 데이터 관리

        [SerializeField] private List<GameObject> slotObjects; // 슬롯 리스트
        // [SerializeField] private List<SkillDataSO> dataObjects = SkillDataDTO.skillDatas; // 데이터 리스트



        public void Init(Dictionary<KeyCode, SkillBase> skills)
        {
            Debug.Log("IngameUI Start");

            skillDatas = skills;

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

        }
        public void UpdateAllSkillSlots()
        {
            if (skillDatas == null)
                return;

            foreach (var key in skillSlots.Keys)
            {
                if (skillDatas.TryGetValue(key, out SkillBase skill))
                {
                    GameObject slot = skillSlots.GetValueOrDefault(key);
                    slot.transform.Find("Image").GetComponent<Image>().sprite = skill.SkillData.Icon;
                    // UpdateSkillSlot(key);
                }
            }
        }

        public void UpdateSkillSlot(KeyCode key)
        {
            // if (skillSlots.TryGetValue(key, out GameObject slot) && skillDatas.TryGetValue(key, out SkillDataSO data))
            // {

            //     slot.GetComponent<Image>().sprite = data.Icon;      // 아이콘 업데이트
            //     // slot.cooldown.text = data.Cooldown;  // 스킬 이름 업데이트
            // }
        }

    }
}
