using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject sphere;

	public float range;

	// Use this for initialization
	void Update () {
		//For testing
		if(Input.GetKeyDown("p")) {Spawn();}
	}

	public void Spawn() 
	{
		GameObject go = Instantiate(sphere) as GameObject;
		go.transform.SetParent(this.transform); 

		go.transform.localPosition = new Vector3(
			Random.Range(0, range), 
			go.transform.position.y, 
			Random.Range(0, range));
	
	go.GetComponent<EnergySphere>().stats = Stats.instance;
	}
}
