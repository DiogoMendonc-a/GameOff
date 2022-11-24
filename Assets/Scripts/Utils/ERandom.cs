using UnityEngine;

public class ERandom {
	public static Vector3 GetRandomPlanarVector() {
		float x = Random.Range(-1.0f, 1.0f);
		float y = Random.Range(-1.0f, 1.0f);
		return new Vector3(x, y, 0).normalized;
	}
}