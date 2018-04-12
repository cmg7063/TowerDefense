using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower {
	public GameObject target;

	public float damage;
	public float fireRate;
	public float currentFireRate;
	public float bulletSpeed;

	public Tower(float damage, float fireRate, float bulletSpeed) {
		this.damage = damage;
		this.fireRate = fireRate;
		this.currentFireRate = fireRate;
		this.bulletSpeed = bulletSpeed;
	}

	public void Update(float dt) {
		currentFireRate -= dt;
	}

	public void resetFireRate() {
		currentFireRate = fireRate;
	}

	public void GetTarget(GameObject currentObject) {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;

		foreach (GameObject enemy in enemies) {
			Vector3 diff = enemy.transform.position - currentObject.transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = enemy;
				distance = curDistance;
			}
		}

		target = closest;
	}
}
