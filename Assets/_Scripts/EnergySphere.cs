using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphere : MonoBehaviour {


	public Stats stats;
	public float lifetime;
	public float energy;
	public int resourceID;


	void Start() 
	{
		Destroy(gameObject, lifetime);
	}

	public void Collect() 
	{
		stats.IncreaseStat(resourceID, energy);
		Destroy(this.gameObject);
	}
}
