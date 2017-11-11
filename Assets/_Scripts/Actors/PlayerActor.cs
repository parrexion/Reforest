using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActor : Actor {

	public Vector2 spawnPosition = new Vector2(0,0);
	public int spawnHeight = 5;

<<<<<<< HEAD
	public SteamVR_TrackedObject cameraRig;
	public SteamVR_TrackedController controllerLeft;
	public SteamVR_TrackedController controllerRight;

	public Text debugText;
	public Text debugText2;
=======
	public WandController controllerLeft;
	public WandController controllerRight;
>>>>>>> 9dde8043189692614248509a9bbc3013f6ed3c62

	// Use this for initialization
	protected override void Initialize() {
		base.Initialize();
		mr.setSpawnHeight(spawnHeight);
	}

    protected override void GetInput() {
		float r = cameraRig.transform.eulerAngles.y;
		debugText2.text = r.ToString();

<<<<<<< HEAD
		currentCooldown -= Time.deltaTime;
		if (currentCooldown > 0)
			return;

		if(controllerLeft.triggerPressed || controllerRight.triggerPressed) {
			debugText.text += "\nTrigger Pressed";
			
			if(r < 45 || r > 315) {
				debugText.text += "\nNorth";
				moveNorth = true;
			} else if(r >= 45 && r < 135) {
				debugText.text += "\nEast";
				moveEast = true;
			} else if(r >= 135 && r < 225) {
				debugText.text += "\nSouth";
				moveSouth = true;
			} else {
				debugText.text += "\nWest";
				moveWest = true;
			}
		}

		Vector2 nextPosition = currentCoordinate;

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || moveWest){
			nextPosition = new Vector2(currentCoordinate.x-1, currentCoordinate.y);
=======
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
>>>>>>> 9dde8043189692614248509a9bbc3013f6ed3c62
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

