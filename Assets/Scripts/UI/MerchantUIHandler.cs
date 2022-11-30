using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUIHandler : MonoBehaviour
{
    [System.Serializable]
    public struct MerchantItem {

        public TMPro.TMP_Text title;
        public Image image;
        public TMPro.TMP_Text description;
        public TMPro.TMP_Text price;
        public Button button;
    }

    public Sprite outOfStockSprite;

    public MerchantItem item0;
    public MerchantItem item1;
    public MerchantItem item2;

    void SetEmpty(MerchantItem item) {
        item.title.text = "SOLD OUT";
        item.image.sprite = outOfStockSprite;
        item.description.text = "";
        item.price.text = "--- G";
        item.button.interactable = false;
    }

    void SetItem(MerchantItem item, Obtainable obtainable, int price) {
        item.title.text = obtainable.title;
        item.image.sprite = obtainable.sprite;
        item.description.text = obtainable.description;
        item.price.text = price.ToString() + " G";
        item.button.interactable = true;

        Weapon w = obtainable as Weapon;
		if(w != null && PlayerClass.instance.inventory.HasItem<StickToYourGun>()) {
			item.button.interactable = false;
		}
    }

    public void SetObtainables(Obtainable o0, Obtainable o1, Obtainable o2, int[] prices) {
        if(o0 == null) {
            SetEmpty(item0);
        }
        else {
            SetItem(item0, o0, prices[0]);
        }

        if(o1 == null) {
            SetEmpty(item1);
        }
        else {
            SetItem(item1, o1, prices[1]);
        }

        if(o2 == null) {
            SetEmpty(item2);
        }
        else {
            SetItem(item2, o2, prices[2]);
        }
    }
}
