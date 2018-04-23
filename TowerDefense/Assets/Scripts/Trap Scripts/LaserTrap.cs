using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : Trap {
	private GameObject target;
	private int frameCounter;

	private float laserRange;
	private float enemyDistance;

	private Color laserColor;
	private float colorChangeValue;

	public bool isActive;

	// Use this for initialization
	void Start () {
		frameCounter = 0;
		laserRange = 5f;
		enemyDistance = Mathf.Infinity;

		laserColor = Color.white;
		colorChangeValue = -0.01f;

		isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCounter % 10 == 0) {
			GetTarget ();
		}
			
		if (target && enemyDistance <= laserRange) {
			isActive = true;
			Rotate ();
			shiftColor ();
		} else {
			isActive = false;
		}

		LaserState ();

		frameCounter++;
	}

	void shiftColor() {
		foreach (Transform child in transform) {
			if (laserColor.r >= 1) {
				colorChangeValue = -0.01f;
			} else if (laserColor.r <= 0) {
				colorChangeValue = 0.01f;
			}

			laserColor.r += colorChangeValue;
			laserColor.g += colorChangeValue / 4;
			laserColor.b += colorChangeValue / 4;
				
			child.gameObject.GetComponent<SpriteRenderer> ().color = laserColor;
		}
	}

	void LaserState() {
		if (isActive) {
			foreach (Transform child in transform) {
				Color visible = child.gameObject.GetComponent<SpriteRenderer> ().color;
				visible.a = 1;

				child.gameObject.GetComponent<SpriteRenderer> ().color = visible;
			}
		} else {
			foreach (Transform child in transform) {
				Color visible = child.gameObject.GetComponent<SpriteRenderer> ().color;
				visible.a = 0;

				child.gameObject.GetComponent<SpriteRenderer> ().color = visible;
			}
		}
	}

	private void Rotate() {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Quaternion rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

		transform.rotation = rotation;
	}

	private void GetTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject closest = null;
		float dist = Mathf.Infinity;

		foreach (GameObject enemy in enemies) {
			float curDistance = Vector2.Distance (enemy.transform.position, transform.position);

			if (curDistance < dist) {
				closest = enemy;
				dist = curDistance;
			}
		}

		enemyDistance = dist;
		target = closest;
	}
}
