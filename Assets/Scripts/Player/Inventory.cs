using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int money { private set; get; }

	public List<Item> itens { private set; get; }
	public Weapon weapon { private set; get; }

	void AddItem(Item item) {
		itens.Add(item);
	}

	void SetWeapon(Weapon weapon) {
		//TODO
	}

	void AddMoney(int value) {
		foreach (Item item in itens)
		{
			item.OnGetMoney(value);
		}
		money += value;
	}

	void RemoveMoney(int value) {
		money += value;
	}

}