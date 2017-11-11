using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor {

	public float movementCooldown = 2.0f;
	private float currentCooldown;
	public Vector2 spawnPosition = new Vector2(0,0);
	public int spawnHeight = 5;

	// Use this for initialization
	protected override void Initialize() {
		base.Initialize();
		mr.setSpawnHeight(spawnHeight);
	}

    protected override void GetInput() {

		currentCooldown -= Time.deltaTime;
		if (currentCooldown > 0)
			return;

		Vector2 nextPosition = currentCoordinate;

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			nextPosition = new Vector2(currentCoordinate.x-1, currentCoordinate.y);
			nextDirection = Direction.WEST;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			nextPosition = new Vector2(currentCoordinate.x+1, currentCoordinate.y);
			nextDirection = Direction.EAST;
		}
		else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			nextPosition = new Vector2(currentCoordinate.x, currentCoordinate.y+1);
			nextDirection = Direction.NORTH;
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			nextPosition = new Vector2(currentCoordinate.x, currentCoordinate.y-1);
			nextDirection = Direction.SOUTH;
		}

		if (nextDirection == Direction.NONE)
			return;

		if (!mr.IsWalkable(nextPosition)){
			nextDirection = Direction.NONE;
			Debug.Log("Could not walk");
		}
		else {
			currentCooldown = movementCooldown;
		}
    }
}

