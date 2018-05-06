using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {
	public Sprite idleSprite;
    public Sprite attackSprite;

	private bool isAttackSprite;
	private int frameCounter;

    // Use this for initialization
    override public void Start() {
        base.Start();
		isAttackSprite = false;
		frameCounter = 0;
    }

	protected override void CheckAlive() {
		if (health <= 0) {
			Destroy(gameObject);

			GameObject clone = scrap;
			clone.GetComponent<ScrapScript> ().scrapValue = scrapDrop;
			Instantiate(scrap, this.transform.position, this.transform.rotation);

			GameUI.scoreTotal += 50;
		}
	}

    protected override void UpdateState() {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > minDistance && distance < maxDistance) {
            state = EnemyState.Pursue;
        } else if (distance > maxDistance) {
            state = EnemyState.Look;
        } else if (distance > 0) {
            state = EnemyState.Stop;
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;

		FlipSprite (vecToPlayer);

        // State specific changes
        if (state == EnemyState.Pursue) {
			transform.Translate(vecToPlayer.normalized * Time.deltaTime * speed, Space.World);
        }

		UpdateFrameCounter ();
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
		isAttackSprite = !isAttackSprite;

		if (isAttackSprite) {
			gameObject.GetComponent<SpriteRenderer>().sprite = attackSprite;
		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
		}
	}
}
