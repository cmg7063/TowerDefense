using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject EnemyBullet;
    public float timeBetweenShots = 1.0f;
    private float timestamp = 0.0f;

	// Use this for initialization
	override public void Start ()
    {
        base.Start();
	}

    protected override void UpdateState()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > minDistance && distance < maxDistance && Time.time > timestamp)
        {
            state = EnemyState.Shoot;
        }
        else if (distance > maxDistance)
        {
            state = EnemyState.Look;
        }
        else if (distance > 0)
        {
            state = EnemyState.Stop;
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        // State specific changes
        if (state == EnemyState.Pursue)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (state == EnemyState.Look)
        {
        }
        else if (state == EnemyState.Stop)
        {
        }
        else if (state == EnemyState.Shoot)
        {
            Vector3 direction = player.transform.position - transform.position;
            float a = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(a, Vector3.forward);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, q, 180);

            GameObject bullet = EnemyBullet;
            Instantiate(bullet, transform.position, rotation);

            timestamp = Time.time + timestamp;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
    }
}
