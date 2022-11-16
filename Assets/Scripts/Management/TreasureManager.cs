using UnityEngine;

public class TreasureManager : MonoBehaviour, IGeneratable {

	public Obtainable loot { private set; get; }
	bool used = false;

	void IGeneratable.Generate(int seed) { 
		loot = LootManager.instance.GetLoot(seed);
	}

	public Obtainable Get() {
		if(used) {
			Debug.LogWarning("Trying to Use Treasure that Had Already Been Used!");
		}
		used = true;
		return loot;
	}
}