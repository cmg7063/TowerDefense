using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
	public float preferredDistance;

    public GameObject EnemyBullet;

	public Sprite idleSprite;
    public Sprite shootSprite;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

	// Use this for initialization
	override public void Start () {
        base.Start();
	}

    protected override void UpdateState() {
        float distance = Vector2.Distance(player.transform.position, transform.position);

		if (distance <= minDistance) {
			state = EnemyState.Flee;
		} else if (distance > minDistance && distance <= preferredDistance) {
			state = EnemyState.Shoot;
		} else if (distance > preferredDistance && distance <= maxDistance) {
			state = EnemyState.Pursue;
        } else if (distance > maxDistance) {
            state = EnemyState.Look;
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        // State specific changes
		if (state == EnemyState.Flee) {
			IsFleeing (vecToPlayer);
		} else if (state == EnemyState.Shoot) {
			IsShooting (vecToPlayer, angle);
		} else if (state == EnemyState.Pursue) {
			IsPursing (vecToPlayer);
        }
    }

	void IsFleeing(Vector2 vecToPlayer) {
		transform.Translate(-vecToPlayer.normalized * Time.deltaTime * speed);
		gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
	}

	void IsShooting(Vector2 vecToPlayer, float angle) {
		gameObject.GetComponent<SpriteRenderer>().sprite = shootSprite;

		if(Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			float spread = Random.Range(-10, 10);
			Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle + spread));
			GameObject bullet = (GameObject)GameObject.Instantiate(EnemyBullet, transform.position, q);
		}
	}

	void IsPursing(Vector2 vecToPlayer) {
		transform.Translate(vecToPlayer.normalized * Time.deltaTime * speed, Space.World);
		gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
	}
}
