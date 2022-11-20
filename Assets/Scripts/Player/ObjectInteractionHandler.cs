using UnityEngine;
using System.Collections.Generic;
public class ObjectInteractionHandler : MonoBehaviour {
	
	List<Interactable> interactablesInRange;

	public GameObject interactionIndicator;

	Interactable active;

	private void Awake() {
		interactablesInRange = new List<Interactable>();	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.name);
		Interactable ii = other.GetComponent<Interactable>();
		if(ii != null) {
			if(!interactablesInRange.Contains(ii) && ii.interactionEnabled)
				interactablesInRange.Add(ii);
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		Interactable ii = other.GetComponent<Interactable>();
		if(ii != null) {
			if(interactablesInRange.Contains(ii))
				interactablesInRange.Remove(ii);
		}
	}

	public void Trigger() {
		if(active == null) return;
		active.Activate();
 	}

	private void Update() {
		if(interactablesInRange.Count == 0) {
			interactionIndicator.SetActive(false);
			active = null;
			return;
		}
		
		Interactable closest = null;
		float distance = float.MaxValue;

		foreach (Interactable ii in interactablesInRange)
		{
			if(!ii.interactionEnabled) continue;
			float newDistance = Vector3.Distance(ii.transform.position, this.transform.position);
			if(newDistance < distance) {
				closest = ii;
				distance = newDistance;
			}	
		}

		if(closest == null) {
			interactionIndicator.SetActive(false);
			active = null;
			return;
		}

		active = closest;
		interactionIndicator.SetActive(true);
		interactionIndicator.transform.position = active.transform.position;

		if(Input.GetKeyDown(KeyCode.E)) {
			active.Activate();
		}
	}
}