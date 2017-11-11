using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor {

	public Vector2 spawnPosition = new Vector2(0,0);
	public int spawnHeight = 5;

	public WandController controllerLeft;
	public WandController controllerRight;

	// Use this for initialization
	protected override void Initialize() {
		base.Initialize();
		mr.setSpawnHeight(spawnHeight);
	}

    protected override void GetInput() {

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			nextDirection = Direction.WEST;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			nextDirection = Direction.EAST;
		}
		else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			nextDirection = Direction.NORTH;
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			nextDirection = Direction.SOUTH;
		}

		if(controllerLeft.upButtonDown || controllerRight.upButtonDown) {
			
		}

		if (nextDirection == Direction.NONE)
			return;
		Vector2 nextPosition = GetNextPositionFromDirection(nextDirection);

		if (!mr.IsWalkable(nextPosition)){
			nextDirection = Direction.NONE;
			Debug.Log("Could not walk");
		}
	}
}

