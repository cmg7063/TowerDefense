using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour {
	public Sprite[] animations;

	private int currentAnimation;
	private int frameCounter;

	// Use this for initialization
	void Start () {
		currentAnimation = 0;
		frameCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCounter % 20 == 0) {
			currentAnimation++;
			if (currentAnimation >= animations.Length) {
				currentAnimation = 0;
			}

			gameObject.GetComponent<SpriteRenderer>().sprite = animations[currentAnimation];
		}

		frameCounter++;
		if (frameCounter >= 60) {
			frameCounter = 0;
		}
	}
}
