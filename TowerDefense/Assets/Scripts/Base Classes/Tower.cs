using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Buildable {
	public GameObject bulletPrefab;

	public GameObject target;
	public float enemyDistance;

	public float damage;
	public float fireRate;
	public float currentFireRate;
	public float bulletSpeed;
	public float bulletLife;
	public float sightRange;

	public Tower() {
		this.buildingName = "TBD";
		this.scrapCost = 1;
		this.damage = 1;
		this.fireRate = 1;
		this.currentFireRate = 1;
		this.bulletSpeed = 1;
		this.bulletLife = 1;
		this.sightRange = 1;
	}

	public Tower(string name, int scrapCost,  float damage, float fireRate, float bulletSpeed, float bulletLife, float sightRange) {
		this.buildingName = name;
		this.scrapCost = scrapCost;
		this.damage = damage;
		this.fireRate = fireRate;
		this.currentFireRate = fireRate;
		this.bulletSpeed = bulletSpeed;
		this.bulletLife = bulletLife;
		this.sightRange = sightRange;
	}

	public void resetFireRate() {
		currentFireRate = fireRate;
	}

	virtual public void Rotate() {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Quaternion rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

		transform.rotation = rotation;
	}

	public void GetTarget(GameObject currentObject) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
        GameObject closest = null;
		float distance = Mathf.Infinity;

	    foreach (GameObject enemy in enemies) {
			float curDistance = Vector2.Distance (enemy.transform.position, currentObject.transform.position);

		    if (curDistance < distance) {
			    closest = enemy;
			    distance = curDistance;
		    }
	    }

		enemyDistance = distance;
		target = closest;
	}
}
