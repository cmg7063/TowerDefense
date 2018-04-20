using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject scrap;
	List<GameObject> onTraps = new List<GameObject>();

    public float health;
	public float speed;

    public float minDistance;
    public float maxDistance;

	private EnemyState state = EnemyState.Look;
    public enum EnemyState {
        Look,
        Pursue,
        Stop
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
//        health = 100;
//		speed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            Destroy(gameObject);
            Instantiate(scrap, this.transform.position, this.transform.rotation);
        }

		UpdateState ();

		DamageFromTrap ();
    }

	// Check State and update it
	void UpdateState() {
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
		Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

		// State specific changes
		if (state == EnemyState.Pursue) {
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		} else if (state == EnemyState.Look) {

		} else if (state == EnemyState.Stop) {

		}

		// common changes
		// TO-DO: Should try and move this out to Update function
		transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
	}
		
	void DamageFromTrap() {
		for (int i = 0; i < onTraps.Count; i++) {
			GameObject trap = onTraps [i];

			if (trap.tag == "SpikeTrap") {
				health -= trap.GetComponent<SpikeTrap> ().damagePerSec * Time.deltaTime;
			}
		}
			
		if (onTraps.Count > 0) {
//			Debug.Log ("Enemy is taking daamge from " + onTraps.Count + " traps");
		}
	}

    void OnTriggerEnter2D(Collider2D collider) {
		// check by tags
        if (collider.gameObject.tag == "TowerBullet") {
			health -= collider.gameObject.GetComponent<Bullet> ().damage;
            Destroy(collider.gameObject);

//            Debug.Log("Taking damage from bullet. Enemy health: " + health);
        }

		// check by layers
		int trapLayer = LayerMask.NameToLayer("Traps");
		if (collider.gameObject.layer == trapLayer) {
			onTraps.Add (collider.gameObject);

//            Debug.Log("Enemy is on a trap.");
        }
    }

	void OnTriggerExit2D(Collider2D other) {
		int trapLayer = LayerMask.NameToLayer("Traps");

		if (other.gameObject.layer == trapLayer) {
			// remove enemy object
			int index = -1;
			for (int i = 0; i < onTraps.Count; i++) {
				index = i;
				if (onTraps [i].GetInstanceID () == other.transform.GetInstanceID ())
					break;
			}

			// ocurrence was found
			if (index >= 0 && index < onTraps.Count) {
				onTraps.RemoveAt (index);

//				Debug.Log ("Enemy is off a trap.");
			}
		}
	}
}
