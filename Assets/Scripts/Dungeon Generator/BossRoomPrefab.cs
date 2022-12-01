using UnityEngine;

public class BossRoomPrefab : RoomPrefab {
	public GameObject nextLevelDoor;
	public Boss boss;

	public override void Init(int seed)
	{
		base.Init(seed);
		boss.nextLevelEntrance = nextLevelDoor;

		enemies.Add(boss);

		boss.transform.SetParent(GameManager.instance.transform);
		boss.transform.rotation = Quaternion.identity;			

		nextLevelDoor.transform.SetParent(GameManager.instance.transform);
		nextLevelDoor.transform.rotation = Quaternion.identity;			
	}
}