using UnityEngine;

public class LetsSplitBehaviour : EnemieClass
{
    public GameObject child;

	public override void OnDie()
	{
		base.OnDie();
        if(child == null) return;
        GameObject.Instantiate(child, this.transform.position, Quaternion.identity);
        GameObject.Instantiate(child, this.transform.position, Quaternion.identity);
	}
}
