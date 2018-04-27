using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTower : Tower {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rotate ();

		if (currentFireRate < 0) {
			Rotate ();
			Fire ();
		} else {
			currentFireRate -= Time.deltaTime;
		}
	}
		
	// This tower has a set rotation speed
	override public void Rotate() {
		transform.Rotate (0, 0, 10 * Time.deltaTime);
	}

	private void Fire() {
		Vector3 dir = new Vector3();

		for (int i = 0; i < 8; i++) {
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (i * 45);
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

			GameObject clone = bulletPrefab;
			clone.GetComponent<Bullet> ().damage = damage;
			clone.GetComponent<Bullet> ().speed = bulletSpeed;
			clone.GetComponent<Bullet> ().life = bulletLife;

			Vector3 pos = transform.position;
			pos.z = -1;

			Instantiate (clone, pos, Quaternion.RotateTowards (transform.rotation, q, 180));
		}

		resetFireRate ();
	}
}
