using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTower : Tower {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFireRate < 0) {
			Fire ();
		} else {
			currentFireRate -= Time.deltaTime;
		}
	}

	private void Fire() {
		Vector3 dir = new Vector3();
		float startDeg = Random.Range (0f, 45f);

		for (int i = 0; i < 8; i++) {
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (i * 45) + startDeg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

			GameObject clone = bulletPrefab;
			clone.GetComponent<Bullet> ().damage = damage;
			clone.GetComponent<Bullet> ().speed = bulletSpeed;
			clone.GetComponent<Bullet> ().life = bulletLife;

			Instantiate (clone, transform.position, Quaternion.RotateTowards (transform.rotation, q, 180));
		}

		resetFireRate ();
	}
}
