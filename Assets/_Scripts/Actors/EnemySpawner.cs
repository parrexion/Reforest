using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	private MapRepresentation mr;

	public Transform firePrefab;
	public Transform lumberPrefab;
	public float spawnInterval;
	private float currentTime;

	// Use this for initialization
	void Start () {
		mr = MapRepresentation.instance;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if (currentTime <= 0) {
			SpawnLumber();
			SpawnFire();
		}
	}

	/// <summary>
	/// Spawns a fire enemy and initializes it.
	/// </summary>
	void SpawnFire() {
		currentTime = spawnInterval;
		int r = Random.Range(0,mr.spawnLocations.Count);
		Vector2 pos = mr.spawnLocations[r];
		Transform fire = Instantiate(firePrefab);
		FireActor fireActor = fire.GetComponent<FireActor>();
		fireActor.currentCoordinate = pos;
		fireActor.travelDirection = MapUtility.FaceFromEdge(pos);
		fireActor.Initialize();
	}

	/// <summary>
	/// Spawns a lumber jack enemy and initializes it.
	/// </summary>
	void SpawnLumber() {
		currentTime = spawnInterval;
		int r = Random.Range(0,mr.spawnLocations.Count);
		Vector2 pos = mr.spawnLocations[r];
		Transform lumber = Instantiate(lumberPrefab);
		LumberActor lumberActor = lumber.GetComponent<LumberActor>();
		lumberActor.currentCoordinate = pos;
		lumberActor.Initialize();
	}

}
