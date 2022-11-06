using UnityEngine;

public class RoomPrefab : MonoBehaviour {
	public bool connectsNorth;
	public bool connectsWest;
	public bool connectsEast;
	public bool connectsSouth;
	public Vector2 boundingBox;
	public GameObject[] entrances;

	public Vector3 displacement;
	public Quaternion rotation;

	public Rect GetBoundingBox(Vector3 displacement, Quaternion rotation) {
		bool sideways = (rotation.eulerAngles.z / 90) % 2 == 1;
		this.displacement = displacement;
		this.rotation = rotation;
		Rect rect = new Rect();
		if(sideways) {
			rect.x = displacement.x - boundingBox.y/2;
			rect.y = displacement.y - boundingBox.x/2;
			rect.width = boundingBox.y;
			rect.height = boundingBox.x;
		}
		else {
			rect.x = displacement.x - boundingBox.x/2;
			rect.y = displacement.y - boundingBox.y/2;
			rect.width = boundingBox.x;
			rect.height = boundingBox.y;
		}

		return rect;
	}

	public Rect GetBoundingBox() {
		return GetBoundingBox(displacement, rotation);
	}

	private void OnDrawGizmos() {
		Rect bb = GetBoundingBox();
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(
			new Vector3(bb.x + bb.width/2, bb.y + bb.height/2, 0),
			new Vector3(bb.width, bb.height, 0));
	}

}