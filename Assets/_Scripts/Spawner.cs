using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform sphere;
	public float range;

	// Use this for initialization
	void Update () {
		//For testing
		//Sif(Input.GetKeyDown("p")) {Spawn();}
	}

	/// <summary>
	/// Spawns a new EnergySphere.
	/// </summary>
	public void Spawn() 
	{
		if(Stats.instance.CurrentSpawnedResources < Stats.instance.MaxSpawnedResources) {	
			Transform t = Instantiate(sphere)	;
			t.SetParent(this.transform); 

			t.localPosition = new Vector3(Random.Range(0, range), t.transform.position.y, Random.Range(0, range));
			Stats.instance.CurrentSpawnedResources++;
		}
	}
}
