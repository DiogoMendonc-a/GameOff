using UnityEngine;

public class ResourcesManager : MonoBehaviour {
	public static ResourcesManager instance;

	void Awake() {
		instance = this;
	}

	public GameObject moneyObj;

}