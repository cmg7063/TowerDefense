using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public Sprite attackSprite;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
    }

    protected override void UpdateState()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > minDistance && distance < maxDistance)
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
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            gameObject.GetComponent<SpriteRenderer>().sprite = attackSprite;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
    }
}
