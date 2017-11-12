using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

	public static int uuid = 0;

	protected MapRepresentation mr;
	public bool isEnemy;		
  	public Vector2 currentCoordinate;
  	protected Direction nextDirection;
 	public float movementCooldown = 2.0f;		
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
		transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);
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
	}

	public float GetPercentCooldownFilled(){
		return 1f - currentCooldown / movementCooldown;
	}

	/// <summary>
	/// Implement this to decide how the actor moves.
	/// </summary>
	protected abstract void GetInput();

	void MoveActor() {
		if (nextDirection == Direction.NONE)		
 			return;
 		
 		Vector2 nextPosition = GetNextPositionFromDirection(nextDirection);		
 		if (isEnemy && mr.HasTrees(nextPosition)){		
 			mr.getTile(nextPosition).tree.TakeDamage();
			isAttacking = true;	
 		}		
 		else if (mr.IsWalkable(nextPosition)) {
			if (isEnemy) {
				int res = mr.getTile(nextPosition).RegisterAtTile(this);
				if (res != -1) {
					gridPosition = res;
					mr.getTile(currentCoordinate).UnregisterAtTile(id);
					currentCoordinate = nextPosition;
					transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);
				}
				Debug.Log("Grid position is now: " + gridPosition);
			}
			else {
 				currentCoordinate = nextPosition;		
 				transform.position = mr.CalculatePositionFromCoordinate(currentCoordinate, gridPosition);		
			}
			isAttacking = false;
 		}		
 		nextDirection = Direction.NONE;
 		currentCooldown = movementCooldown;
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

