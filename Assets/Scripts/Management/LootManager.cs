using UnityEngine;

public class LootManager : MonoBehaviour {
	public static LootManager instance;

	void Awake() {
		instance = this;
	}

	public Obtainable[] lootTable;

	public Obtainable GetLoot(int seed) {
		return lootTable[seed%lootTable.Length];
	}
}