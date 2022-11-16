using UnityEngine;

public class TreasureUIManagerDebugger : MonoBehaviour {
	void Update() {
		if(Input.GetKeyDown(KeyCode.T)) {
			InGameUIManager.instance.ActivateTreasureUI(LootManager.instance.GetLoot(0), HandleResponse);
		}
	}

	void HandleResponse(bool answer) {
		if(answer) {
			Debug.Log("Taken");
		}
		else {
			Debug.Log("Not Taken");
		}
	}
}