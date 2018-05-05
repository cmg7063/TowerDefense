using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy {
	public Sprite idleSprite;
	public Sprite idleSprite2;

    public GameObject EnemyBullet;
	public int maxShotNum;
	public float minBulletSpeed;
	public float maxBulletSpeed;

	private bool isSecondSprite;
	private int frameCounter;

    // Use this for initialization
    override public void Start() {
        base.Start();
		isSecondSprite = false;
		frameCounter = 0;
	}

	protected override void CheckAlive() {
		if (health <= 0) {
			Explode ();
			Instantiate(scrap, this.transform.position, this.transform.rotation);
			GameUI.scoreTotal += 50;
		}
	}

    protected override void UpdateState() {
        float distance = Vector2.Distance(player.transform.position, transform.position);

		if (distance <= minDistance) {
			state = EnemyState.Explode;
		} else if (distance > minDistance && distance <= maxDistance) {
			state = EnemyState.Pursue;
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;

        // State specific changes
		if (state == EnemyState.Explode) {
			Explode ();
		} else if (state == EnemyState.Pursue) {
			transform.Translate(vecToPlayer.normalized * Time.deltaTime * speed, Space.World);
        }

		UpdateFrameCounter ();
    }

	void Explode() {
		Vector2 vecToPlayer = (player.transform.position - transform.position);
		float angle = Mathf.Atan2 (vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;

		// more health = more bullets fired when exploding
		// min shot number = 50% of max
		float shotNum = (maxShotNum / 2) + ((health / maxHealth) * (maxShotNum / 2));

		for (int i = 0; i < shotNum; i++) {
			float spread = Random.Range (-360, 360);
			Quaternion q = Quaternion.Euler (new Vector3 (0, 0, angle + spread));
			Vector3 pos = transform.position;
			GameObject clone = EnemyBullet;

			pos.z = -6;
			clone.GetComponent<Bullet> ().speed = Random.Range (minBulletSpeed, maxBulletSpeed);

			Instantiate (clone, pos, q);
		}

		Destroy (gameObject);
	}

	void UpdateFrameCounter() {
		if (frameCounter % 30 == 0) {
			ToggleSprite ();
		}

		frameCounter++;
		if (frameCounter >= 60) {
			frameCounter = 0;
		}
	}

	void ToggleSprite() {
		isSecondSprite = !isSecondSprite;

		if (isSecondSprite) {
			gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite2;
		}
	}
}
