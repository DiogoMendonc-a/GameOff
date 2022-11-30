using UnityEngine;

public class Money : MonoBehaviour {
	public int value;
	public float speed;
	public float range;

	void OnTriggerEnter2D(Collider2D other) {
		PlayerClass pc = other.GetComponent<PlayerClass>();
		if(pc != null) {
			pc.inventory.AddMoney(value);
			Destroy(gameObject);
		}
	}

	void Update() {
		if(Vector3.Distance(PlayerClass.instance.transform.position, this.transform.position) < range) {
			transform.Translate((PlayerClass.instance.transform.position - this.transform.position).normalized * Time.deltaTime * speed);
		}
	}
}