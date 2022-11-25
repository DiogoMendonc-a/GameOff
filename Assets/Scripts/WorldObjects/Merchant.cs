using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : Interactable, IGeneratable
{
    bool[] selling = { false, false, false };
    Obtainable[] stock = { null, null, null };
	public int[] price = { 0, 0, 0 };

    void IGeneratable.Generate(int seed) {
        System.Random rng = new System.Random(seed);
        for (int i = 0; i < stock.Length; i++)
        {
            selling[i] = true;
            stock[i] = LootManager.instance.GetLoot(rng.Next());
            float modifier = rng.Next(75, 150) / 100.0f;
			price[i] = Mathf.FloorToInt(stock[i].basePrice * modifier * PlayerClass.instance.MERCHANT_PRICES_MODIFIER);
        }
    }

    void CallUI() {
		Obtainable o0 = null;
		if(selling[0]) o0 = stock[0];
		Obtainable o1 = null;
		if(selling[1]) o1 = stock[1];
		Obtainable o2 = null;
		if(selling[2]) o2 = stock[2];
		InGameUIManager.instance.ActivateMerchantUI(o0, o1, o2, HandleResponse);
	}

    public override void Activate() {
        CallUI();
    }
    
    void HandleResponse(int answer) {
        PlayerClass.instance.inventory.AddObtainable(stock[answer]);
		selling[answer] = false;
		CallUI();
	}
}
