using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotTower : MonoBehaviour {
	public GameObject bulletPrefab;

	public float damage;
	public float fireRate;
	public float bulletSpeed;

	private Tower spreadShotTower;

	// Use this for initialization
	void Start () {
		spreadShotTower = new Tower (damage, fireRate, bulletSpeed);
	}

	// Update is called once per frame
	void Update () {
		if (spreadShotTower.currentFireRate < 0) {
			spreadShotTower.GetTarget (gameObject);
			Fire();
		}

		spreadShotTower.Update (Time.deltaTime);
	}

	void Fire() {
		Vector3 dir = spreadShotTower.target.transform.position - transform.position;

		for (int i = 0; i < 3; i++) {
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 85f - (i * 5);
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			GameObject clone = bulletPrefab;
			clone.GetComponent<Bullet> ().speed = bulletSpeed;

			Instantiate (clone, transform.position, Quaternion.RotateTowards (transform.rotation, q, 180));
		}

		spreadShotTower.resetFireRate ();
	}
}
