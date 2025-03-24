using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class InventoryUI_ScrollData : Gpm.Ui.InfiniteScrollData
    {
        public InventoryUI_ScrollData(string itemName, int itemGrade)
        {
            ItemName = itemName;
            ItemGrade = itemGrade;
        }

        public string ItemName { get; set; }
        public int ItemGrade { get; set; }

    }
}
