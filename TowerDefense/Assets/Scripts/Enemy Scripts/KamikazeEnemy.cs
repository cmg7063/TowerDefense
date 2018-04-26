using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy
{
    public GameObject EnemyBullet;
    public float timeBetweenShots = 1.0f;
    private float timestamp = 0.0f;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
	}

    protected override void UpdateState()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > minDistance && distance < maxDistance && Time.time > timestamp)
        {
            state = EnemyState.Pursue;
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
            Vector3 direction = player.transform.position - transform.position;

            for (int i = 0; i < 3; i++)
            {
                float a = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 85f - (i * 5);
                Quaternion q = Quaternion.AngleAxis(a, Vector3.forward);

                GameObject clone = EnemyBullet;
                //clone.GetComponent<Bullet>().damage = damage;
                //clone.GetComponent<Bullet>().speed = bulletSpeed;
                //clone.GetComponent<Bullet>().life = bulletLife;

                Instantiate(clone, transform.position, Quaternion.RotateTowards(transform.rotation, q, 180));
            }
        }
        else if (state == EnemyState.Look)
        {
        }
        else if (state == EnemyState.Stop)
        {
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
    }
}
