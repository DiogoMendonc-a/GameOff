using UnityEngine;
using System.Collections.Generic;
public class RoomPrefab : MonoBehaviour {
	public Vector2 boundingBox;
	public BoundingBox[] boundingBoxes;
	public GameObject[] entrances;

	public Vector3 displacement;
	public Quaternion rotation;

	public Rect[] GetBoundingBoxes(Vector3 displacement, Quaternion rotation) {
		this.displacement = displacement;
		this.rotation = rotation;
		List<Rect> rects = new List<Rect>();
		foreach(BoundingBox bb in boundingBoxes) {
			rects.Add(bb.GetRect(displacement, rotation));
		}
		return rects.ToArray();
	}

	public Rect[] GetBoundingBoxes() {
		return GetBoundingBoxes(displacement, rotation);
	}

	private void OnDrawGizmos() {
		Rect[] bbs = GetBoundingBoxes();
		Gizmos.color = Color.red;
		foreach (Rect bb in bbs)
		{
			Gizmos.DrawWireCube(
				new Vector3(bb.x + bb.width/2, bb.y + bb.height/2, 0),
				new Vector3(bb.width, bb.height, 0));
		}
	}

}

[System.Serializable]
public class BoundingBox {
	public float width;
	public float height;
	public float centerX;
	public float centerY;

	public Rect GetRect(Vector3 displacement, Quaternion rotation) {
		Rect rect = new Rect();
		Vector3 newCenter = rotation * new Vector3(centerX, centerY, 0);
		bool sideways = (rotation.eulerAngles.z / 90) % 2 == 1;
		if(sideways) {
			rect.x = displacement.x + newCenter.x - height/2;
			rect.y = displacement.y + newCenter.y - width/2;
			rect.width = height;
			rect.height = width;
		}
		else {
			rect.x = displacement.x + newCenter.x - width/2;
			rect.y = displacement.y + newCenter.y - height/2;
			rect.width = width;
			rect.height = height;
		}

		return rect;

	}
}