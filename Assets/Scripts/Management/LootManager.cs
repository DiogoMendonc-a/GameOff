using UnityEngine;

public class LootManager : MonoBehaviour {
	static Obtainable[] lootTable;

	public static Obtainable GetLoot(int seed) {
		return lootTable[seed%lootTable.Length];
	}
}