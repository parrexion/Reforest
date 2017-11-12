using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

	public static int uuid = 0;

	protected MapRepresentation mr;
	public bool isEnemy;
  	public Vector2 currentCoordinate;
	public Vector3 previousWorldPosition;
	public Vector3 currentWorldPosition;
  	protected Direction nextDirection;
 	public float movementCooldown = 0.5f;
 	public float currentCooldown;
	public bool isAttacking;
	public int id;
	public int gridPosition = -1;

	public Gauge gauge;

	// Use this for initialization
	void Start () {
		if (!isEnemy)
			Initialize();
	}

	public virtual void Initialize() {
		mr = GameObject.Find("MapGenerator").GetComponent<MapRepresentation>();
		gridPosition = mr.getTile(currentCoordinate).RegisterAtTile(this);
		if (isEnemy) {
			if (gridPosition == -1) {
				Destroy(gameObject);
				return;
			}
		}

		transform.position = currentWorldPosition = previousWorldPosition = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);
		id = uuid++;
		if (isEnemy)
			currentCooldown = movementCooldown;
	}
	
	void Update() {
		if (!isEnemy) 
		{
			gauge.Visualize(currentCooldown, movementCooldown);
		}
		currentCooldown -= Time.deltaTime;		
 		if (currentCooldown > 0) {
 			return;		
 		}
		GetInput();
	}

	void FixedUpdate () {
		MoveActor();
		LerpToNextPosition();
	}

	public float GetPercentCooldownFilled(){
		return 1f - Mathf.Clamp01(currentCooldown / movementCooldown);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "projectile") {
			if(isEnemy) {
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
		}
	}

	protected abstract void GetInput();

	void MoveActor() {
		if (nextDirection == Direction.NONE){
			if (currentCooldown <= 0)
				previousWorldPosition = currentWorldPosition;
 			return;
		}	
 		
 		Vector2 nextPosition = GetNextPositionFromDirection(nextDirection);		
 		if (isEnemy && mr.HasTrees(nextPosition)){		
 			mr.getTile(nextPosition).tree.TakeDamage();
			isAttacking = true;
			previousWorldPosition = currentWorldPosition;
 		}		
 		else if (mr.IsWalkable(nextPosition)) {
			if (isEnemy) {
				int res = mr.getTile(nextPosition).RegisterAtTile(this);
				if (res != -1) {
					gridPosition = res;
					mr.getTile(currentCoordinate).UnregisterAtTile(id);
					currentCoordinate = nextPosition;
					previousWorldPosition = currentWorldPosition;
					currentWorldPosition = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);
					// transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);
				}
				Debug.Log("Grid position is now: " + gridPosition);
			}
			else {
 				currentCoordinate = nextPosition;
				previousWorldPosition = currentWorldPosition;	
 				currentWorldPosition = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);	
 				// transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);	
			}
			isAttacking = false;
 		}		
 		nextDirection = Direction.NONE;
 		currentCooldown = movementCooldown;
 	}

	void LerpToNextPosition(){
		if (isEnemy)
			transform.position = Vector3.Lerp(previousWorldPosition,currentWorldPosition,GetPercentCooldownFilled());
		else
			transform.position = currentWorldPosition;
	}
 		
 		
 	protected Vector2 GetNextPositionFromDirection(Direction nextDir) {
		switch (nextDirection) {
			case Direction.NORTH:
				return new Vector2(currentCoordinate.x, currentCoordinate.y+1);
			case Direction.WEST:
				return new Vector2(currentCoordinate.x-1, currentCoordinate.y);
			case Direction.EAST:
				return new Vector2(currentCoordinate.x+1, currentCoordinate.y);
			case Direction.SOUTH:
				return new Vector2(currentCoordinate.x, currentCoordinate.y-1);
			default:		
 				return currentCoordinate;
		}

	}

}

