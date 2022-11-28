using UnityEngine;

public class LetsSplitBehaviour : EnemyClass
{
    public GameObject child;

	public float attack_velocity;

	public float windUpTime = 0.333f; 
	public float windDownTime = 0.25f; 
	public float timeMoves = 0.75f; 
	public float movement_time = 0.6333f;
	public float attack_range = 2;
	public int attack_damage;
	public int touch_damage;
	int current_damage;
	float t_move = 0;
	float t = 0;

	protected override void UpdateState()
	{
		if(HP <= 0) {
			state = MOVE_FLAG.DIE;
			return;
		}
		if(state == MOVE_FLAG.MOVE) {
			if(Vector3.Distance(PlayerClass.instance.transform.position, transform.position) < attack_range) {
				state = MOVE_FLAG.ATACK;
				return;
			}
		}
	}

	protected override void DoAttackBehaviour()
	{
		current_damage = touch_damage;
		if(t == 0) {
			animator.CrossFade("LetsSplitAttack", 0, 0, 0);
		}
		t += Time.deltaTime;
		if(t < windUpTime) {
			rb.velocity = Vector2.zero;
		}
		else if(t < windUpTime + timeMoves) {
			current_damage = attack_damage;
			if(direction == null || direction == Vector2.zero) {
				direction = PlayerClass.instance.transform.position - transform.position;
			}
			rb.velocity = direction.normalized * attack_velocity;
		}
		else if(t < windUpTime + timeMoves + windDownTime) {
			rb.velocity = Vector2.zero;
			current_damage = touch_damage;
		}
		else{
			direction = Vector3.zero;
			t = 0;
			state = MOVE_FLAG.MOVE;
		}
	}

	protected override void DoMoveBehaviour()
	{
		current_damage = touch_damage;
		if(t_move == 0) {
			animator.CrossFade("LetsSplitMove", 0, 0, 0);
		}
		t_move += Time.deltaTime;
		if(t_move < movement_time) {
			if(direction == null || direction == Vector2.zero) {
				direction = PlayerClass.instance.transform.position - transform.position;
			}
			rb.velocity = direction.normalized * MOV_SPEED;
		}
		else if (t_move < 1.0f){
			rb.velocity = Vector2.zero;
		}
		else {
			direction = Vector2.zero;
			t_move = 0;
		}
	}

	protected override void DoDieBehaviour()
	{
		base.DoDieBehaviour();
        if(child == null) return;
        GameObject.Instantiate(child, this.transform.position, Quaternion.identity);
        GameObject.Instantiate(child, this.transform.position, Quaternion.identity);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		PlayerClass player = other.collider.GetComponent<PlayerClass>();
        if (player != null)
        {
            player.ChangeHp(-Mathf.FloorToInt(current_damage));
        }
	}
}






