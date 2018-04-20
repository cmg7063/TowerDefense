using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTower : Tower {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFireRate <= 0) {
			GetTarget (gameObject);

			if (target) {
				Fire();
			}
		}

		currentFireRate -= Time.deltaTime;
	}

	private void Fire() {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Quaternion rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

		GameObject clone = bulletPrefab;
		clone.GetComponent<Bullet> ().damage = damage;
		clone.GetComponent<Bullet> ().speed = bulletSpeed;

		Instantiate(clone, transform.position, rotation);

		resetFireRate ();
	}
}
