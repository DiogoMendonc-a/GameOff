using UnityEngine;

public class Boss : EnemyClass {
	[HideInInspector]
	public GameObject nextLevelEntrance;

	protected override void DoDieBehaviour()
	{
		base.DoDieBehaviour();
		nextLevelEntrance.SetActive(true);
	}

}