using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor {

	public float movementCooldown = 2.0f;
	private float currentCooldown;
	private bool moveNorth = false;
	private bool moveSouth = false;
	private bool moveEast = false;
	private bool moveWest = false;
	public Vector2 spawnPosition = new Vector2(0,0);
	public int spawnHeight = 5;

	public Transform cameraRig;
	public WandController controllerLeft;
	public WandController controllerRight;

	// Use this for initialization
	protected override void Initialize() {
		base.Initialize();
		mr.setSpawnHeight(spawnHeight);
	}

    protected override void GetInput() {

		currentCooldown -= Time.deltaTime;
		if (currentCooldown > 0)
			return;

		if(controllerLeft.upButtonDown || controllerRight.upButtonDown || cameraRig == null) {

			float r = cameraRig.transform.eulerAngles.y;
			if(r < 45 || r > 315) {
				Debug.Log("North");
				moveNorth = true;
			} else if(r >= 45 && r < 135) {
				Debug.Log("East");
				moveEast = true;
			} else if(r >= 135 && r < 225) {
				Debug.Log("South");
				moveSouth = true;
			} else {
				Debug.Log("West");
				moveWest = true;
			}
		}

		Vector2 nextPosition = currentCoordinate;

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || moveWest){
			nextPosition = new Vector2(currentCoordinate.x-1, currentCoordinate.y);
			nextDirection = Direction.WEST;
			moveWest = false;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || moveEast){
			nextPosition = new Vector2(currentCoordinate.x+1, currentCoordinate.y);
			nextDirection = Direction.EAST;
			moveEast = false;
		}
		else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || moveNorth){
			nextPosition = new Vector2(currentCoordinate.x, currentCoordinate.y+1);
			nextDirection = Direction.NORTH;
			moveNorth = false;
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || moveSouth){
			nextPosition = new Vector2(currentCoordinate.x, currentCoordinate.y-1);
			nextDirection = Direction.SOUTH;
			moveSouth = false;
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

