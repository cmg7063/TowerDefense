using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLocation : MonoBehaviour {
	public GameObject player;

	private int collidingWith;
	private Color spriteColor;

	// Use this for initialization
	void Start () {
		spriteColor = gameObject.GetComponent<SpriteRenderer> ().color;
		spriteColor.a = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBuildLocation ();

		if (collidingWith <= 0) {
			player.GetComponent<Player>().validLocation = true;

			spriteColor.r = 0f;
			spriteColor.g = 1f;
		} else {
			player.GetComponent<Player>().validLocation = false;

			spriteColor.r = 1f;
			spriteColor.g = 0f;
		}

		gameObject.GetComponent<SpriteRenderer> ().color = spriteColor;

	}

	void UpdateBuildLocation() {
		Vector3 position = player.transform.position;
		float xOffset = 1.0f;
		if (player.GetComponent<Player>().facingLeft) {
			xOffset = -1.0f;
		}

		position.x = Mathf.Floor (position.x) + 0.5f + xOffset;
		position.y = Mathf.Floor (position.y) + 0.5f;
		position.z = 0;

		gameObject.transform.position = position;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		int trapLayer = LayerMask.NameToLayer("Traps");
		int towerLayer = LayerMask.NameToLayer("Towers");

		if (collider.gameObject.layer == trapLayer || collider.gameObject.layer == towerLayer) {
			collidingWith += 1;

//			Debug.Log ("buildLocation entered Collision");
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		int trapLayer = LayerMask.NameToLayer("Traps");
		int towerLayer = LayerMask.NameToLayer("Towers");

		if (collider.gameObject.layer == trapLayer || collider.gameObject.layer == towerLayer) {
			collidingWith -= 1;

//			Debug.Log ("buildLocation exited Collision ");
		}
	}
}
