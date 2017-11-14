using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphere : MonoBehaviour {

	public Stats.Resource type;
	public float lifetime;
	public float energy;


	void Start() 
	{
		Destroy(gameObject, lifetime);
	}
	
	void OnMouseOver() 
	{
		Collect();
	}

	public void Collect() 
	{
		Stats.instance.IncreaseStat(type, energy);
		Destroy(this.gameObject);
	}

	void OnDestroy()
	{
		Stats.instance.CurrentSpawnedResources--;
	}
}
