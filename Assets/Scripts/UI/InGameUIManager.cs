using UnityEngine;
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

	public GameObject treasureObject;
	public GameObject merchantObject;
	Action<bool> treasureCallback;
	Action<int> merchantCallback;

	public void ActivateTreasureUI(Obtainable treasure, Action<bool> treasureCallback) {
		treasureObject.GetComponent<TreasureUIHandler>().SetObtainable(treasure);
		treasureObject.SetActive(true);
		this.treasureCallback = treasureCallback;
	}

	public void ActivateMerchantUI(Obtainable obtainable0, Obtainable obtainable1, Obtainable obtainable2, Action<int> merchantCallback) {
		merchantObject.GetComponent<MerchantUIHandler>().SetObtainables(obtainable0, obtainable1, obtainable2);
		merchantObject.SetActive(true);
		this.merchantCallback = merchantCallback;
	}

	public void CloseTreasure(bool taken) {
		treasureObject.SetActive(false);
		treasureCallback.Invoke(taken);
	}

	public void Buy(int index) {
		merchantCallback.Invoke(index);
	}

	public void CloseMerchant() {
		merchantObject.SetActive(false);
	}

}