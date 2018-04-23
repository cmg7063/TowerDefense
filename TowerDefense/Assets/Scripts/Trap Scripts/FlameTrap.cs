using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : Trap {
	public bool isActive;
	public float fireRate;
	private float currentFireRate;
	public float flameLife;
	private float currentFlameLife;

	// Use this for initialization
	void Start () {
		isActive = false;
		currentFireRate = fireRate;
		currentFlameLife = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFlameLife <= 0 && currentFireRate <= 0) {
			isActive = true;

			currentFireRate = fireRate;
			currentFlameLife = flameLife;
		} else if (currentFlameLife > 0) {
			currentFlameLife -= Time.deltaTime;
		} else {
			isActive = false;
			currentFireRate -= Time.deltaTime;
		}

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
}
