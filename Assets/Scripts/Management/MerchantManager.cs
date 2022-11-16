using UnityEngine;

public class MerchantManager : MonoBehaviour, IGeneratable {
	static int numberOfItems = 3;
	public Obtainable[] loot { private set; get; }
	public int[] price { private set; get; }
	bool[] bought;
	void IGeneratable.Generate(int seed) {
		loot = new Obtainable[numberOfItems];
		bought = new bool[numberOfItems];
		price = new int[numberOfItems];

		System.Random rng = new System.Random(seed);

		for (int i = 0; i < numberOfItems; i++)
		{
			loot[i] = LootManager.instance.GetLoot(rng.Next());
			float modifier = rng.Next(75, 150) / 100.0f;
			price[i] = Mathf.FloorToInt(loot[i].baseValue * modifier);
			bought[i] = false;
		}
	}

	//Obtainable Buy(int id) {
	//	if(bought[id]) {
	//		Debug.LogWarning("Trying to Buy Item that Had Already Been Bought!");
	//		return null;
	//	}
	//	if(Player.money > price[id]) {
	//		bought[id] = true;
	//		return loot[id];
	//	}
	//	else{
	//		Debug.LogWarning("Trying to Buy Item without Enouhg Money!");
	//		return null;
	//	}
	//}

}