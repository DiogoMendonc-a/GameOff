using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int money { private set; get; }

	public List<Item> itens { private set; get; }
	public Weapon weapon { private set; get; }

	private void Awake() {
		itens = new List<Item>();
	}

	public void AddItem(Item item) {
		itens.Add(item);
	}


	public void SetWeapon(Weapon weapon) {
		//TODO
	}

	public void AddObtainable(Obtainable o) {
		Item i = o as Item;
		if(i != null) {
			AddItem(i);
		}

		Weapon w = o as Weapon;
		if(w != null) {
			SetWeapon(w);
		}
	}

	public void AddMoney(int value) {
		foreach (Item item in itens)
		{
			item.OnGetMoney(value);
		}
		money += value;
	}

	public void RemoveMoney(int value) {
		money += value;
	}

}