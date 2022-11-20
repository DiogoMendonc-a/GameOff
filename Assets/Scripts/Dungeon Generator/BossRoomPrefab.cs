using UnityEngine;

public class BossRoomPrefab : RoomPrefab {
	public GameObject nextLevelDoor;
	public Boss boss;

	public override void Init(int seed)
	{
		base.Init(seed);
		boss.nextLevelEntrance = nextLevelDoor;
	}
}