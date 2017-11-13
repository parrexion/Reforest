using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActor : Actor {

	public Vector2 spawnPosition = new Vector2(0,0);

	[Header("Bullet")]
	public GameObject bulletPrefab;
	public float bulletAquaCost = 3f;
	public float bulletSolarCost = 0f;

	[Header("Steam VR")]
	public SteamVR_TrackedObject cameraRig;
	public SteamVR_TrackedObject controllerRightTransform;
	public SteamVR_TrackedController controllerLeft;
	public SteamVR_TrackedController controllerRight;

	public Gauge movementGuage;


	// Use this for initialization
	public override void Initialize() {
		base.Initialize();
		gridPosition = -1;
		movementGuage.maxValue = movementCooldown;
	}

    protected override void GetInput() {

		//Update movementGauge
		movementGuage.UpdateVisualValue(currentCooldown);

		float r = cameraRig.transform.eulerAngles.y;
		Direction faceDirection = Direction.NONE;
		if(controllerLeft.triggerPressed) {
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
		} else if (controllerRight.triggerPressed && bulletPrefab != null){
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

		nextPosition = new Vector2(currentCoordinate.x-1, currentCoordinate.y);

		if (!mr.IsWalkable(nextPosition, this)){
			nextDirection = Direction.NONE;
			Debug.Log("Could not walk");
		}
	}

	void FireBullet(float aquaCost, float solarCost){
		if(Stats.instance.CanAfford(Stats.Resource.AQUA, aquaCost) && solarCost <= Stats.instance.currentRes[1] ) 
		{
			//FIRE A BULLET
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = controllerRightTransform.transform.position; 
			bullet.transform.rotation = controllerRightTransform.transform.rotation;
			currentCooldown = 0.5f;

			Stats.instance.DecreaseStat(Stats.Resource.AQUA, aquaCost);
			Stats.instance.DecreaseStat(Stats.Resource.SUN, solarCost);

			Debug.Log("Aqua left: " + Stats.instance.currentRes[0]);
			Debug.Log("Solar left: " + Stats.instance.currentRes[1]);
		}else 
		{
			Debug.Log("Not enought resources to shoot");
		}
	}
}

