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
 	public float actionCooldown = 3f;
 	public float currentCooldown;
	public bool isAttacking;
	public int id;
	public int gridPosition = -1;


	// Use this for initialization
	void Start () {
		if (!isEnemy)
			Initialize();
	}

	public virtual void Initialize() {
		mr = GameObject.Find("MapGenerator").GetComponent<MapRepresentation>();
		if (isEnemy) {
			gridPosition = mr.getTile(currentCoordinate).RegisterAtTile(this);
			if (gridPosition == -1) {
				Destroy(gameObject);
				return;
			}
		}

		transform.position = currentWorldPosition = previousWorldPosition = MapUtility.ConvertCoordinateToWorldPosition(currentCoordinate, gridPosition);
		id = uuid++;
		if (isEnemy)
			currentCooldown = actionCooldown;
	}
	
	protected virtual void Update() {

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
		return 1f - Mathf.Clamp01(currentCooldown / actionCooldown);
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
 		
 		Vector2 nextPosition = MapUtility.GetNextPositionFromDirection(currentCoordinate, nextDirection);		
 		if (isEnemy && mr.HasTrees(nextPosition)){		
 			mr.getTile(nextPosition).tree.TakeDamage();
			isAttacking = true;
			previousWorldPosition = currentWorldPosition;
			currentCooldown = actionCooldown;
			nextDirection = Direction.NONE;
			return;
 		}		
 		else if (mr.IsWalkable(nextPosition,this)) {
			if (isEnemy) {
				int res = mr.getTile(nextPosition).RegisterAtTile(this);
				if (res != -1) {
					gridPosition = res;
					mr.getTile(currentCoordinate).UnregisterAtTile(id);
					currentCoordinate = nextPosition;
					previousWorldPosition = currentWorldPosition;
					currentWorldPosition = MapUtility.ConvertCoordinateToWorldPosition(currentCoordinate, gridPosition);
					nextDirection = Direction.NONE;
					currentCooldown = actionCooldown;
					isAttacking = false;
					return;
				}
			}
			else {
 				currentCoordinate = nextPosition;
				previousWorldPosition = currentWorldPosition;	
 				currentWorldPosition = MapUtility.ConvertCoordinateToWorldPosition(currentCoordinate, gridPosition);	
			}
			isAttacking = false;
 		}		
 		nextDirection = Direction.NONE;
 		currentCooldown = actionCooldown;
 	}

	void LerpToNextPosition(){
		if (isEnemy) {
			transform.LookAt(currentWorldPosition);
			transform.position = Vector3.Lerp(previousWorldPosition,currentWorldPosition,GetPercentCooldownFilled());
		}
		else {
			transform.position = currentWorldPosition;
		}
	}

}

