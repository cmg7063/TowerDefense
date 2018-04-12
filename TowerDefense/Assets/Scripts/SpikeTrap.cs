using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {
	// TO-DO
	// MOVE DAMAGE LOGIC TO ENEMY
	// Doing damage logic here has problems if enemy object is destroyed
	// it will not be removed from the list and break
	List<GameObject> enemies;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log(enemies.Count);
		for (int i = 0; i < enemies.Count; i++) {
			DealDamage(enemies[i]);
			bool checkActive = enemies [i].activeInHierarchy;
			Debug.Log(checkActive);
		}
	}

	// TO-DO: ADD DAMAGE LOGIC
	void DealDamage(GameObject enemy) {
		//enemy.GetComponent<Enemy>()
	}

	void OnTriggerEnter2D(Collider2D other) {
		// add enemy object
		if (other.gameObject.tag == "Enemy") {
			enemies.Add (other.gameObject);
			Debug.Log ("ADDED ENEMY");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			// remove enemy object
			int index = -1;
			for (int i = 0; i < enemies.Count; i++) {
				index = i;
				if (enemies [i].GetInstanceID () == other.transform.GetInstanceID ())
					break;
			}

			// ocurrence was found
			if (index >= 0 && index < enemies.Count) {
				enemies.RemoveAt (index);
				Debug.Log ("REMOVED ENEMY");
			}
		}
	}
}
