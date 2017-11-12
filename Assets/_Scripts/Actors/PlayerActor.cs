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
	public float bulletAquaCost = 3f;
	public float bulletSolarCost = 0f;

	public GameObject bulletPrefab;
	public Text text1;
	public Text text2;
	public AudioSource shoot;

	public SteamVR_TrackedObject cameraRig;
	public SteamVR_TrackedObject controllerRightTransform;
	public SteamVR_TrackedController controllerLeft;
	public SteamVR_TrackedController controllerRight;

	// Use this for initialization
	public override void Initialize() {
		base.Initialize();
		gridPosition = -1;
		mr.setSpawnHeight(spawnHeight);
	}

    protected override void GetInput() {
		float r = cameraRig.transform.eulerAngles.y;
			text1.text = r.ToString();

		if(controllerLeft.triggerPressed) {
			// CHECK THE ROTATION OF THE HMD
			
			if(r < 45 || r > 315) {
				 text2.text += "\nNorth";
				moveNorth = true;
			} else if(r >= 45 && r < 135) {
				 text2.text += "\nEast";
				moveEast = true;
			} else if(r >= 135 && r < 225) {
				 text2.text += "\nSouth";
				moveSouth = true;
			} else {
				 text2.text += "\nWest";
				moveWest = true;
			}
		} else if (controllerRight.triggerPressed && bulletPrefab != null){
			FireBullet(bulletAquaCost, bulletSolarCost);
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

	void FireBullet(float aquaCost, float solarCost){
		if(aquaCost <= Stats.instance.resources[0] && solarCost <= Stats.instance.resources[1] ) 
		{
			//FIRE A BULLET
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = controllerRightTransform.transform.position; 
			bullet.transform.rotation = controllerRightTransform.transform.rotation;
			currentCooldown = 0.5f;

			Stats.instance.DecreaseStat(0, aquaCost);
			Stats.instance.DecreaseStat(1, solarCost);

			Debug.Log("Aqua left: " + Stats.instance.resources[0]);
			Debug.Log("Solar left: " + Stats.instance.resources[1]);
			shoot.Play();
		}else 
		{
			Debug.Log("Not enought resources to shoot");
		}
	}
}

