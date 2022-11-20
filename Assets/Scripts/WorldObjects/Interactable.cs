using UnityEngine;

public class Interactable : MonoBehaviour {
	public bool interactionEnabled = true;

	public virtual void Activate() {
		Debug.LogWarning("Interactable Activation Method Not Overriden");
	}
}