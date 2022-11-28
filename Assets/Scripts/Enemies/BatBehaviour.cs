using UnityEngine;

public class BatBehaviour : EnemyClass
{
    public float circleRange;
    public float move_velocity;
    public float attack_velocity;
    public float move_time;
    public int damage;
    float r = 0;
    float a = 0;

    Vector3 target;

    bool inited = false;

    protected override void UpdateState() {
        if(HP <= 0) {
			state = MOVE_FLAG.DIE;
			return;
		}

        if(state != MOVE_FLAG.INACTIVE && !inited) {
            Invoke("SetAttack", move_time);
            inited = true;
        }
    }

	protected override void DoMoveBehaviour()
	{
        float angular_velocity = move_velocity / circleRange;
        Vector3 playerDir = transform.position - PlayerClass.instance.transform.position;
		Vector3 targetDirection = Quaternion.AngleAxis(angular_velocity * 3f,Vector3.forward) * playerDir.normalized;
        target = PlayerClass.instance.transform.position + targetDirection * circleRange;

        rb.velocity = (target - transform.position).normalized * move_velocity;

        a = circleRange;
	}

	protected override void DoAttackBehaviour()
	{
        float angular_velocity = move_velocity * 2 / a;
        Vector3 playerDir = transform.position - PlayerClass.instance.transform.position;
		Vector3 targetDirection = Quaternion.AngleAxis(angular_velocity * 3f,Vector3.forward) * playerDir.normalized;
        target = PlayerClass.instance.transform.position + targetDirection * a;

        rb.velocity = (target - transform.position).normalized * move_velocity * 2;
        a -= attack_velocity * Time.deltaTime;
	}

    void SetAttack() {
        Debug.Log("Attacking");
        state = MOVE_FLAG.ATACK;
    }

	private void OnCollisionEnter2D(Collision2D other) {
		PlayerClass player = other.collider.GetComponent<PlayerClass>();
        if (player != null)
        {
            player.ChangeHp(-Mathf.FloorToInt(damage));
            state = MOVE_FLAG.MOVE;
            Invoke("SetAttack", move_time);
        }
	}

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(target, 0.2f);
    }
}
