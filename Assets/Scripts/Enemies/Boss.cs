using UnityEngine;

public class Boss : EnemyClass {
	[HideInInspector]
	public GameObject nextLevelEntrance;

	public override void OnDie()
	{
		base.OnDie();
		nextLevelEntrance.SetActive(true);
	}

}