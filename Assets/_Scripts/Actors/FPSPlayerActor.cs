using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSPlayerActor : Actor {

	public Vector2 spawnPosition = new Vector2(0,0);

	[Header("Bullet")]
	public GameObject bulletPrefab;
	public float bulletAquaCost = 3f;
	public float bulletSolarCost = 0f;
	public AudioSource shoot;

	[Header("Camera stuff")]
	public Camera playerCamera;
	public float turnSpeedMultiplier;


	// Use this for initialization
	public override void Initialize() {
		base.Initialize();
		gridPosition = -1;
	}

	protected override void Update() {
		float dir = Input.GetAxis("Horizontal") * turnSpeedMultiplier;
		Vector3 rotateValue = new Vector3(0, dir * -1, 0);
		transform.eulerAngles = transform.eulerAngles - rotateValue;
		base.Update();
	}

    protected override void GetInput() {

		float r = playerCamera.transform.eulerAngles.y;
		Direction faceDirection = Direction.NONE;

		if(Input.GetKeyDown(KeyCode.Joystick1Button0)) {
			// CHECK THE ROTATION OF THE HMD
			if(r < 45 || r > 315) {
				faceDirection = Direction.NORTH;
			} else if(r >= 45 && r < 135) {
				faceDirection = Direction.EAST;
			} else if(r >= 135 && r < 225) {
				faceDirection = Direction.SOUTH;
			} else {
				faceDirection = Direction.WEST;
			}
		} else if (Input.GetKeyDown(KeyCode.Joystick1Button1) && bulletPrefab != null){
			FireBullet(bulletAquaCost, bulletSolarCost);
		}

		Vector2 nextPosition = currentCoordinate;

		if (faceDirection != Direction.NONE) {
			nextDirection = faceDirection;
		}
#if UNITY_EDITOR
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
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
#endif
		if (nextDirection == Direction.NONE)
			return;

		nextPosition = MapUtility.GetNextPositionFromDirection(currentCoordinate, nextDirection);

		if (!mr.IsWalkable(nextPosition, this)){
			nextDirection = Direction.NONE;
			Debug.Log("Could not walk  " + nextPosition);
		}
	}

	void FireBullet(float aquaCost, float solarCost){
		if(Stats.instance.CanAfford(Stats.Resource.AQUA, aquaCost) && 
				Stats.instance.CanAfford(Stats.Resource.SUN, solarCost)) {

			//FIRE A BULLET
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = transform.position; 
			bullet.transform.rotation = transform.rotation;
			currentCooldown = 0.5f;

			Stats.instance.DecreaseStat(Stats.Resource.AQUA, aquaCost);
			Stats.instance.DecreaseStat(Stats.Resource.SUN, solarCost);

			Debug.Log("Aqua left: " + Stats.instance.currentRes[0]);
			Debug.Log("Solar left: " + Stats.instance.currentRes[1]);
			shoot.Play();
		} else {
			Debug.Log("Not enought resources to shoot");
		}
	}
}

