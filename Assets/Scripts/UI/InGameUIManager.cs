using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameUIManager : MonoBehaviour {
	public static InGameUIManager instance; 

	private void Awake() {
		if(instance != null) {
			Debug.LogWarning("A InGameUIManager is Already Present! Discarding New Instance.");
			Destroy(this.gameObject);
			return;
		}
		instance = this;
	}

	public bool openMenu = false;

	public Image hpBar;

	public TMPro.TMP_Text clipText;
	public Image reloadImage;

	public TMPro.TMP_Text currencyUI;

	public GameObject inventoryDisplayer;
	public GameObject itemDisplayerPrefab;
	public GameObject tooltip;
	public GameObject treasureObject;
	public GameObject merchantObject;
	public GameObject deathObject;
	public GameObject victoryObject;
	Action<bool> treasureCallback;
	Action<int> merchantCallback;

	public void AddItemDisplay(Item item) {
		GameObject displayer = Instantiate(itemDisplayerPrefab);
		displayer.GetComponent<ItemDisplayer>().Set(item, tooltip);
		displayer.transform.SetParent(inventoryDisplayer.transform);
	}

	public void SetHealth(float fraction) {
		hpBar.fillAmount = fraction;
	}

	public void SetClip(int remaining, int max) {
		clipText.text = remaining + "/" + max;
	}

	public void SetReloadProgress(float value) {
		reloadImage.fillAmount = value;
	}

	public void DisplayCurrency(int amount) {
		currencyUI.text = "" + amount;
	}

	public void ActivateTreasureUI(Obtainable treasure, Action<bool> treasureCallback) {
		openMenu = true;
		treasureObject.GetComponent<TreasureUIHandler>().SetObtainable(treasure);
		treasureObject.SetActive(true);
		this.treasureCallback = treasureCallback;
	}

	public void CloseTreasure(bool taken) {
		openMenu = false;
		treasureObject.SetActive(false);
		treasureCallback.Invoke(taken);
	}

	public void ActivateMerchantUI(Obtainable obtainable0, Obtainable obtainable1, Obtainable obtainable2, int[] prices, Action<int> merchantCallback) {
		openMenu = true;
		merchantObject.GetComponent<MerchantUIHandler>().SetObtainables(obtainable0, obtainable1, obtainable2, prices);
		merchantObject.SetActive(true);
		this.merchantCallback = merchantCallback;
	}

	public void Buy(int index) {
		merchantCallback.Invoke(index);
	}

	public void CloseMerchant() {
		openMenu = false;
		merchantObject.SetActive(false);
	}

	public void ActivateDeathUI() {
		openMenu = true;
		deathObject.SetActive(true);
	}

	public void ActivateVictoryUI() {
		openMenu = true;
		victoryObject.SetActive(true);
	}

	public void ReturnToMainMenu() {
		GameManager.instance.ReturnToMenu();
	}

}