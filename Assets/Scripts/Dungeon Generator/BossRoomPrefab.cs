using UnityEngine;
using System.Collections.Generic;

public class BossRoomPrefab : RoomPrefab {
	public GameObject nextLevelDoor;

	public override void Init(int seed)
	{
		System.Random rng = new System.Random(seed);
		enemies = new List<EnemyClass>();

		GameObject[] layouts;

		if(DungeonGenerator.levelGenerating == 1) {
			layouts = layouts_level2;
		}
		else if(DungeonGenerator.levelGenerating == 2) {
			layouts = layouts_level3;
		}
		else {
			layouts = layouts_level1;
		}
		
		if(layouts.Length == 0) return;

		GameObject layout = layouts[rng.Next()%layouts.Length];
		GameObject spawned = GameObject.Instantiate(layout, this.transform.position, this.transform.rotation);

		List<Transform> children = new List<Transform>();
		
		for(int i = 0; i < spawned.transform.childCount; i++) {
			Transform t = spawned.transform.GetChild(i);
			if(t == spawned.transform) continue;
			EnemyClass enemy = t.GetComponent<EnemyClass>();
			if(enemy != null) {
				enemies.Add(enemy);
				if(enemy.isBoss) {
					enemy.OnDie += (() => { nextLevelDoor.SetActive(true); });
				}
			}
			
			children.Add(t);
		}
		foreach (Transform child in children)
		{
			child.SetParent(GameManager.instance.transform);
			child.rotation = Quaternion.identity;			
		}

		Destroy(spawned);
	}
}