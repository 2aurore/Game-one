using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    [CreateAssetMenu(fileName = "New Item Stat", menuName = "Game One/Item/Create New Item Stat")]
    public class ItemDataSO : ScriptableObject
    {
        [field: SerializeField] public string Item_ID { get; set; }
        [field: SerializeField] public string ItemName { get; set; }
        [field: SerializeField] public string ItemCategory { get; set; }
    }
}
