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
	Action<bool> treasureCallback;

	public void ActivateTreasureUI(Obtainable treasure, Action<bool> treasureCallback) {
		treasureObject.GetComponent<TreasureUIHandler>().SetObtainable(treasure);
		treasureObject.SetActive(true);
		this.treasureCallback = treasureCallback;
	}

	public void CloseTreasure(bool taken) {
		treasureObject.SetActive(false);
		treasureCallback.Invoke(taken);
	}

}