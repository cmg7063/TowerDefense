using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Buildable {
	public GameObject target;
	public GameObject bulletPrefab;

	public float damage;
	public float fireRate;
	public float currentFireRate;
	public float bulletSpeed;

	public Tower() {
		this.buildingName = "TBD";
		this.damage = 1;
		this.fireRate = 1;
		this.currentFireRate = 1;
		this.bulletSpeed = 1;
		this.scrapCost = 1;
	}

	public Tower(string name, float damage, float fireRate, float bulletSpeed, int scrapCost) {
		this.buildingName = name;
		this.damage = damage;
		this.fireRate = fireRate;
		this.currentFireRate = fireRate;
		this.bulletSpeed = bulletSpeed;
		this.scrapCost = scrapCost;
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
