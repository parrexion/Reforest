using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Costs {

	public float aquaticCost;
	public float solarCost;

	public Costs(float aquaCost, float sunCost) {
		this.aquaticCost = aquaCost;
		this.solarCost = sunCost;
	}
}
