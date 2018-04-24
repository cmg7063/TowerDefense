using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject scrap;
	List<GameObject> onTraps = new List<GameObject>();

    public float health;
	public float speed;

    public float minDistance;
    public float maxDistance;

	public EnemyState state = EnemyState.Look;
    public enum EnemyState
    {
        Look,
        Pursue,
        Attack,
        Shoot,
        Stop
    }

	// Use this for initialization
	virtual public void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	protected void Update ()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(scrap, this.transform.position, this.transform.rotation);
        }

		UpdateState ();
		DamageFromTrap ();
    }

    // Check State and update it
    abstract protected void UpdateState();
		
	protected void DamageFromTrap() {
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
			
		if (onTraps.Count > 0) {
            //Debug.Log ("Enemy is taking daamge from " + onTraps.Count + " traps");
		}
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
		// check by tags
		if (collider.gameObject.tag == "TowerBullet")
        {
			health -= collider.gameObject.GetComponent<Bullet> ().damage;
			Destroy (collider.gameObject);

            //Debug.Log("Taking damage from bullet. Enemy health: " + health);
		}
        else if (collider.gameObject.tag == "Flame")
        {
			onTraps.Add (collider.gameObject);
		}
        else if (collider.gameObject.tag == "Laser")
        {
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
