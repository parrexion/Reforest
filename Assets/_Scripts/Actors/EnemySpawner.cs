using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

#region Singleton
    public static EnemySpawner instance;

    protected void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
#endregion

	public Transform firePrefab;
	public List<Vector2> spawnLocations;
	public float spawnInterval;
	private float currentTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if (currentTime <= 0)
			SpawnFire();
	}

	void SpawnFire() {
		currentTime = spawnInterval;
		int r = Random.Range(0,spawnLocations.Count);
		Vector2 pos = spawnLocations[r];
		Transform fire = Instantiate(firePrefab);
		FireActor fireActor = fire.GetComponent<FireActor>();
		fireActor.currentCoordinate = pos;
		fireActor.travelDirection = GetStartingDirection(pos);
	}

	Direction GetStartingDirection(Vector2 coordinate){
		if (coordinate.x == 0)
			return Direction.EAST;
		if (coordinate.y == 0)
			return Direction.NORTH;
		if (coordinate.x == MapRepresentation.instance.size.x-1)
			return Direction.WEST;
		if (coordinate.y == MapRepresentation.instance.size.y-1)
			return Direction.SOUTH;

		return Direction.NONE;
	}
}
