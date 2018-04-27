using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject EnemyBullet;

    public Sprite shootSprite;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

	// Use this for initialization
	override public void Start () {
        base.Start();
	}

    protected override void UpdateState() {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > minDistance && distance < maxDistance) {
            state = EnemyState.Shoot;
        }
        else if (distance > maxDistance) {
            state = EnemyState.Look;
        }
        else if (distance > 0) {
            state = EnemyState.Stop;
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        // State specific changes
        if (state == EnemyState.Pursue) {
			transform.Translate(vecToPlayer.normalized * Time.deltaTime * speed, Space.World);
        }
        else if (state == EnemyState.Shoot) {
			transform.Translate(vecToPlayer.normalized * Time.deltaTime * speed);
            gameObject.GetComponent<SpriteRenderer>().sprite = shootSprite;

            if(Time.time > nextFire) {
                nextFire = Time.time + fireRate;

                float spread = Random.Range(-10, 10);
                Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle + spread));
                GameObject bullet = (GameObject)GameObject.Instantiate(EnemyBullet, transform.position, q);
            }
        }

//        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
    }
}
