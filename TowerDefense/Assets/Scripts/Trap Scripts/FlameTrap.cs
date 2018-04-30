using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : Trap {
	public bool isActive;
	public float fireRate;
	private float currentFireRate;
	public float flameLife;
	private float currentFlameLife;
    
    // flame color
	private Color flameColor;
	private float colorToggle;
	private float flameToggleAmout;
	private float alphaAmount;
	private float alphaToggleAmout;

	// Use this for initialization
	void Start () {
		isActive = false;
		currentFireRate = fireRate;
		currentFlameLife = 0;

		flameColor =  new Color(240f / 255f, 103f / 255f, 14f / 255f, 1);
		colorToggle = 0.0f;
		alphaAmount = 1f;
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
				if (colorToggle <= 0) {
					flameToggleAmout = 0.005f;
				} else if (colorToggle >= 1) {
					flameToggleAmout = -0.005f;
				}

				if (alphaAmount <= 0.5f) {
					alphaToggleAmout = 0.001f;
				} else if (alphaAmount >= 1f) {
					alphaToggleAmout = -0.001f;
				}

				colorToggle += flameToggleAmout;
				alphaAmount += alphaToggleAmout;

				Color current = Color.white;
				current.r = current.r * (1 - colorToggle) + flameColor.r * (colorToggle);
				current.g = current.g * (1 - colorToggle) + flameColor.g * (colorToggle);
				current.b = current.b * (1 - colorToggle) + flameColor.b * (colorToggle);
				current.a = alphaAmount;

				Debug.Log (current.a);

				child.gameObject.GetComponent<SpriteRenderer> ().color = current;
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
