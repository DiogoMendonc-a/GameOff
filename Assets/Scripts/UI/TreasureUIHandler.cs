using UnityEngine;
using UnityEngine.UI;

public class TreasureUIHandler : MonoBehaviour {
	public TMPro.TMP_Text treasureName;
	public TMPro.TMP_Text description;
	public Image image;
	public Button accept;

	public void SetObtainable(Obtainable obt) { 
		treasureName.text = obt.title;
		description.text = obt.description;
		image.sprite = obt.sprite;

		Weapon w = obt as Weapon;
		if(w != null && PlayerClass.instance.inventory.HasItem<StickToYourGun>()) {
			accept.interactable = false;
		}
	}
}