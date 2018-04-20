using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Buildable {
	public float damagePerSec;

	public Trap() {
		this.buildingName = "TBD";
		this.damagePerSec = 1;
		this.scrapCost = 1;
	}

	public Trap(string name, float damagePerSec, int scrapCost) {
		this.buildingName = name;
		this.damagePerSec = damagePerSec;
		this.scrapCost = scrapCost;
	}
}
