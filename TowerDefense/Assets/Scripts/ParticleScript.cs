using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
	private ParticleSystem particle;
	// Use this for initialization
	void Start () {
		particle = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, particle.main.duration);
	}
}
