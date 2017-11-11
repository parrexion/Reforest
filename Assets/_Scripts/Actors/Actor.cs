using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

	protected MapRepresentation mr;
	protected Vector2 currentCoordinate;
	protected Direction nextDirection;

	// Use this for initialization
	void Start () {
		mr = GameObject.Find("MapGenerator").GetComponent<MapRepresentation>();
		currentCoordinate = Vector2.zero;
		transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate);
	}
	
	void Update() {
		GetInput();
	}

	void FixedUpdate () {
		MoveActor();
	}

	/// <summary>
	/// Implement this to decide how the actor moves.
	/// </summary>
	protected abstract void GetInput();

	void MoveActor() {
		switch (nextDirection) {
			case Direction.NONE:
				//Do nothing
				return;
			case Direction.NORTH:
				currentCoordinate = new Vector2(currentCoordinate.x, currentCoordinate.y+1);
				break;
			case Direction.WEST:
				currentCoordinate = new Vector2(currentCoordinate.x-1, currentCoordinate.y);
				break;
			case Direction.EAST:
				currentCoordinate = new Vector2(currentCoordinate.x+1, currentCoordinate.y);
				break;
			case Direction.SOUTH:
				currentCoordinate = new Vector2(currentCoordinate.x, currentCoordinate.y-1);
				break;
		}
		transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate);
		nextDirection = Direction.NONE;
	}

}

