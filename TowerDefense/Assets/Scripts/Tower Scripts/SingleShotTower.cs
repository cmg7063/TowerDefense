using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTower : MonoBehaviour {
	public GameObject bulletPrefab;

	public float damage;
	public float fireRate;
	public float bulletSpeed;

	private Tower singleShotTower;

	// Use this for initialization
	void Start () {
		singleShotTower = new Tower (damage, fireRate, bulletSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (singleShotTower.currentFireRate <= 0) {
			singleShotTower.GetTarget (gameObject);

			if (singleShotTower.target) {
				Fire();
			}
		}

		singleShotTower.Update (Time.deltaTime);
	}

	private void Fire() {
		Vector3 dir = singleShotTower.target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Quaternion rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

		GameObject clone = bulletPrefab;
		clone.GetComponent<Bullet> ().damage = damage;
		clone.GetComponent<Bullet> ().speed = bulletSpeed;

		Instantiate(clone, transform.position, rotation);

		singleShotTower.resetFireRate ();
	}
}
