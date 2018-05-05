using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour {
	public ParticleSystem hurtEffect;

    public GameObject player;
    public GameObject scrap;
	List<GameObject> onTraps = new List<GameObject>();

    public float health;
	public float speed;

    public float minDistance;
    public float maxDistance;

	public EnemyState state = EnemyState.Look;
    public enum EnemyState {
        Look,
        Pursue,
		Flee,
        Attack,
        Shoot,
        Explode,
        Stop
    }

	private float frameCounter;

	// Use this for initialization
	virtual public void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		frameCounter = 0;
	}
	
	// Update is called once per frame
	protected void Update () {
        if (health <= 0) {
            Destroy(gameObject);
            Instantiate(scrap, this.transform.position, this.transform.rotation);
            GameUI.scoreTotal += 50;
        }

		UpdateState ();
		DamageFromTrap ();

		frameCounter++;

		if (frameCounter == 60) {
			frameCounter = 0;
		}
    }

    // Check State and update it
    abstract protected void UpdateState();
		
	protected void DamageFromTrap() {
		if (onTraps.Count > 0 && frameCounter % 30 == 0) {
			HurtEffect ();
		}

		for (int i = 0; i < onTraps.Count; i++) {
			GameObject trap = onTraps [i];

			if (trap.tag == "SpikeTrap") {
				health -= trap.GetComponent<Trap> ().damagePerSec * Time.deltaTime;
			} else if (trap.tag == "Flame") {
				FlameTrap trapScript = trap.gameObject.transform.parent.gameObject.GetComponent<FlameTrap> ();

				if (trapScript.isActive) {
					health -= trapScript.damagePerSec * Time.deltaTime;
				}
			} else if (trap.tag == "Laser") {
				LaserTrap trapScript = trap.gameObject.transform.parent.gameObject.GetComponent<LaserTrap> ();

				if (trapScript.isActive) {
					health -= trapScript.damagePerSec * Time.deltaTime;
				}
			}
		}
	}

	void HurtEffect() {
		// play particle effect
		ParticleSystem clone = hurtEffect;
		Vector3 location = transform.position;
		location.z = -6;

		Instantiate(clone, location, transform.rotation);
	}

    void OnTriggerEnter2D(Collider2D collider) {
		// check by tags
		if (collider.gameObject.tag == "TowerBullet") {
			health -= collider.gameObject.GetComponent<Bullet> ().damage;
			Destroy (collider.gameObject);

			HurtEffect ();
            //Debug.Log("Taking damage from bullet. Enemy health: " + health);
		} else if (collider.gameObject.tag == "Flame") {
			onTraps.Add (collider.gameObject);
		} else if (collider.gameObject.tag == "Laser") {
			onTraps.Add (collider.gameObject);
			Debug.Log("Enemy is on a trap.");
		}

		// check by layers
		int trapLayer = LayerMask.NameToLayer("Traps");
		if (collider.gameObject.layer == trapLayer) {
			onTraps.Add (collider.gameObject);

            //Debug.Log("Enemy is on a trap.");
        }
    }

	void OnTriggerExit2D(Collider2D collider) {
		// check by tags
		if (collider.gameObject.tag == "Flame") {
			// remove enemy object
			int index = -1;
			for (int i = 0; i < onTraps.Count; i++) {
				index = i;
				if (onTraps [i].GetInstanceID () == collider.transform.GetInstanceID ())
					break;
			}

			// ocurrence was found
			if (index >= 0 && index < onTraps.Count) {
				onTraps.RemoveAt (index);
                //Debug.Log ("Enemy is off a trap.");
			}
		} else if (collider.gameObject.tag == "Laser") {
			// remove enemy object
			int index = -1;
			for (int i = 0; i < onTraps.Count; i++) {
				index = i;
				if (onTraps [i].GetInstanceID () == collider.transform.GetInstanceID ())
					break;
			}

			// ocurrence was found
			if (index >= 0 && index < onTraps.Count) {
				onTraps.RemoveAt (index);
                //Debug.Log ("Enemy is off a trap.");
			}
		}

		// check by layers
		int trapLayer = LayerMask.NameToLayer("Traps");
		if (collider.gameObject.layer == trapLayer) {
			// remove enemy object
			int index = -1;
			for (int i = 0; i < onTraps.Count; i++) {
				index = i;
				if (onTraps [i].GetInstanceID () == collider.transform.GetInstanceID ())
					break;
			}

			// ocurrence was found
			if (index >= 0 && index < onTraps.Count) {
				onTraps.RemoveAt (index);

				//Debug.Log ("Enemy is off a trap.");
			}
		}
	}
}
