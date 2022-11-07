using UnityEngine;

[ExecuteInEditMode]
public class GlobalPositionDisplayer : MonoBehaviour {
	public Vector3 globalPosition;
	
	private void Update() {
		globalPosition = transform.position;
	}
}