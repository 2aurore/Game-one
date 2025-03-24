using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class InventoryUI : UIBase
    {
        public Gpm.Ui.InfiniteScroll infiniteScroll;

        void Start()
        {

            for (int i = 0; i < 100; i++)
            {
                infiniteScroll.InsertData(new InventoryUI_ScrollData($"Item {i + 1}", Random.Range(0, 7)));
            }

            infiniteScroll.RefreshScroll();
        }

        [Sirenix.OdinInspector.Button]
        public void OnClickRenewalData()
        {
            var dataList = infiniteScroll.GetDataList();
            int randomIndex = Random.Range(0, dataList.Count);

            var convertData = dataList[randomIndex] as InventoryUI_ScrollData;
            convertData.ItemName = $"Renewal Data {randomIndex + 1}";
            convertData.ItemGrade = Random.Range(0, 7);
        }

        [Sirenix.OdinInspector.Button]
        public void OnClickDeleteRandomIndexData()
        {
            int randomIndex = Random.Range(0, infiniteScroll.GetItemCount());
            infiniteScroll.RemoveData(randomIndex);
        }
    }
}
