using UnityEngine;

public class Interactable : MonoBehaviour {
	public virtual void Activate() {
		Debug.LogWarning("Interactable Activation Method Not Overriden");
	}
}