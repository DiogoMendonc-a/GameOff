using UnityEngine;

public class MerchantUIManagerDebugger : MonoBehaviour {
	public bool[] selling = { false, false, false };

	private void Reset() {
		for (int i = 0; i < selling.Length; i++)
		{
			selling[i] = true;
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.M)) {
			Reset();
			CallUI();
		}
	}

	void CallUI() {
		Obtainable o0 = null;
		if(selling[0]) o0 = LootManager.instance.GetLoot(0);
		Obtainable o1 = null;
		if(selling[1]) o1 = LootManager.instance.GetLoot(0);
		Obtainable o2 = null;
		if(selling[2]) o2 = LootManager.instance.GetLoot(0);
		Debug.LogWarning("Not implemented");
		//InGameUIManager.instance.ActivateMerchantUI(o0, o1, o2, HandleResponse);
	}

	void HandleResponse(int answer) {
		selling[answer] = false;
		CallUI();
	}
}