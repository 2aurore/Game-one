using System.Collections;
using System.Collections.Generic;
using Gpm.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ONE
{
    public class InventoryUI_ScrollItem : Gpm.Ui.InfiniteScrollItem
    {
        public Image gradeImage;
        public TextMeshProUGUI itemNameText;

        public void SetData(string itemName, int grade)
        {
            itemNameText.text = itemName;
            gradeImage.color = Constant.GetItemGradeColor(grade);
        }

        public override void UpdateData(InfiniteScrollData scrollData)
        {
            InventoryUI_ScrollData convertData = scrollData as InventoryUI_ScrollData;
            if (convertData != null)
            {
                SetData(convertData.ItemName, convertData.ItemGrade);
            }
        }
    }
}
