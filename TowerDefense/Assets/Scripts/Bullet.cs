using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float damage;
	public float speed;
	public float life;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			Destroy (gameObject);
		}

		transform.Translate (Vector3.up * speed * Time.deltaTime);

		life -= Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
