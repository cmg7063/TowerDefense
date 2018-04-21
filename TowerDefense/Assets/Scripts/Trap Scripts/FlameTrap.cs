using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : Trap {
	public bool active;
	public float fireRate;
	private float currentFireRate;
	public float flameLife;
	private float currentFlameLife;

	// Use this for initialization
	void Start () {
		active = false;
		currentFireRate = fireRate;
		currentFlameLife = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFlameLife <= 0 && currentFireRate <= 0) {
			active = true;

			currentFireRate = fireRate;
			currentFlameLife = flameLife;
		} else if (currentFlameLife > 0) {
			currentFlameLife -= Time.deltaTime;
		} else {
			active = false;
			currentFireRate -= Time.deltaTime;
		}

		if (active) {
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
