using UnityEngine;
using UnityEngine.EventSystems;
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
                itemNum.text = item.itemAmount.ToString();
        }
}

/*public void OnPointerClick(PointerEventData eventData) {
        if (!item)
        {
                Debug.Log("No item here yet!");
                return;
        }
        Debug.Log("Name : " + item.itemName + " Amount : " + item.itemAmount);
}*/