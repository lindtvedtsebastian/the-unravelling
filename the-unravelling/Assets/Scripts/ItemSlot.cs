using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
        ItemData item;

        public Image itemImg;
        public Text itemNum;

        public void AddItem(ItemData newItem)
        {
                item = newItem;
                itemImg.sprite = item.preview;
                itemImg.enabled = true;
                itemNum.enabled = true;
                itemNum.text = "";
                itemNum.text = item.itemAmount.ToString();
        }
}