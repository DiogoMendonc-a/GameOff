using UnityEngine;
using System.Collections.Generic;
public class RoomPrefab : MonoBehaviour {
	public BoundingBox[] boundingBoxes;
	public GameObject[] entrances;

	public GameObject[] layouts;

	public Vector3 displacement;
	public Quaternion rotation;

	public List<EnemyClass> enemies;

	bool visited = false;

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

	public virtual void Init(int seed) {
		System.Random rng = new System.Random(seed);
		enemies = new List<EnemyClass>();

		if(layouts.Length == 0) return;
		GameObject layout = layouts[rng.Next()%layouts.Length];
		GameObject spawned = GameObject.Instantiate(layout, this.transform.position, this.transform.rotation);
		foreach(Transform t in spawned.GetComponentsInChildren<Transform>()) {
			if(t == spawned.transform) continue;
			EnemyClass enemy = t.GetComponent<EnemyClass>();
			if(enemy != null) enemies.Add(enemy);

			t.parent = GameManager.instance.transform;
			t.rotation = Quaternion.identity;
		}
		Destroy(spawned);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.GetComponent<PlayerClass>() != null) {
			if(!visited) {
				if(PlayerClass.instance.inventory.HasItem<RunForYourMoney>())
					PlayerClass.instance.inventory.AddMoney(RunForYourMoney.amountGiven);
				visited = true;
			}

			foreach (EnemyClass enemy in enemies)
			{
				enemy.state = EnemyClass.MOVE_FLAG.MOVE;
			}
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