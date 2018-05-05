using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy {
    public GameObject EnemyBullet;

    // Use this for initialization
    override public void Start() {
        base.Start();
	}

    protected override void UpdateState() {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= maxDistance) {
            state = EnemyState.Pursue;
        } else if (distance > maxDistance) {
            state = EnemyState.Look;
        } else if (distance > 0) {
            state = EnemyState.Stop;
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        // State specific changes
        if (state == EnemyState.Pursue) {
			transform.Translate(vecToPlayer.normalized * Time.deltaTime * speed, Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        Vector2 vecToPlayer = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;

        if (coll.gameObject.tag == "Player") {
            for(int i = 0; i < 10; i++) {
                float spread = Random.Range(-360, 360);
                Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle + spread));
                GameObject bullet = (GameObject)GameObject.Instantiate(EnemyBullet, transform.position, q);
            }

            Destroy(gameObject);
        }
    }
}
