using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap {
	public float damagePerSecTemp;
	public int scrapCostTemp;

	private Trap spikeTrap;
	// Use this for initialization
	void Start () {
		spikeTrap = new Trap ("Spike Trap", damagePerSecTemp, scrapCostTemp);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
