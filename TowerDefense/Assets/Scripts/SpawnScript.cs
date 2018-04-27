using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
	public Vector3 minVector;
	public Vector3 maxVector;
	public Vector3 velocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (velocity.x != 0 && transform.position.x < minVector.x) {
			velocity.x = Mathf.Abs (velocity.x);
		} else if (velocity.x != 0 && transform.position.x > maxVector.x) {
			velocity.x = -Mathf.Abs (velocity.x);
		}

		if (velocity.y != 0 && transform.position.y < minVector.y) {
			velocity.y = Mathf.Abs (velocity.y);
		} else if (velocity.y != 0 && transform.position.y > maxVector.y) {
			velocity.y = -Mathf.Abs (velocity.y);
		}
			
		transform.Translate (velocity * Time.deltaTime);
	}
}
