using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActor : Actor {

	private bool moveNorth = false;
	private bool moveSouth = false;
	private bool moveEast = false;
	private bool moveWest = false;
	public Vector2 spawnPosition = new Vector2(0,0);
	public int spawnHeight = 5;

	public SteamVR_TrackedObject cameraRig;
	public SteamVR_TrackedController controllerLeft;
	public SteamVR_TrackedController controllerRight;

	// public Text debugText;
	// public Text debugText2;

	// Use this for initialization
	protected override void Initialize() {
		base.Initialize();
		mr.setSpawnHeight(spawnHeight);
	}

    protected override void GetInput() {
		float r = cameraRig.transform.eulerAngles.y;
			// debugText2.text = r.ToString();

		if(controllerLeft.triggerPressed || controllerRight.triggerPressed) {
			// debugText.text += "\nTrigger Pressed";
			
			if(r < 45 || r > 315) {
				// debugText.text += "\nNorth";
				moveNorth = true;
			} else if(r >= 45 && r < 135) {
				// debugText.text += "\nEast";
				moveEast = true;
			} else if(r >= 135 && r < 225) {
				// debugText.text += "\nSouth";
				moveSouth = true;
			} else {
				// debugText.text += "\nWest";
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
	}
}

