using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {

	private MapRepresentation mr;
	public bool canMove = true;
	public float movementCooldown = 2.0f;
	private float cooldown;
	private bool hasJustMoved = false;
	public Vector2 spawnPosition = new Vector2(0,0);
	public int spawnHeight = 5;
	// private Vector3 tempPos = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		mr = GameObject.Find("MapGenerator").GetComponent<MapRepresentation>();
		mr.setSpawnHeight(spawnHeight);
		transform.position = mr.CalculatePositionFromCoordinate(new Vector2(spawnPosition.x,spawnPosition.y));
		cooldown = 0f;
	}
	
	void Update()
	{
		Timer();
		//MoveActor();
	}

	void FixedUpdate () {
		
		//UpdatePos(tempPos);
		MoveActor();

		if(Input.GetKey(KeyCode.Space)) {
			//	
		}
	}

	void UpdatePos(Vector3 pos) {
		if(pos != new Vector3(0,0,0)) {
			transform.position = pos;
		}
	}

	void MoveActor() {
		
		if(canMove) {	
			if(Input.GetKey(KeyCode.RightArrow)) {
				transform.position += Vector3.right * 10;
				canMove = false;
				hasJustMoved = true;
			} else if(Input.GetKey(KeyCode.LeftArrow)) {
				transform.position += Vector3.left * 10;
				canMove = false;
				hasJustMoved = true;
			} else if(Input.GetKey(KeyCode.UpArrow)) {
				transform.position += Vector3.forward * 10;
				canMove = false;
				hasJustMoved = true;
			} else if(Input.GetKey(KeyCode.DownArrow)) {
				transform.position += Vector3.back * 10;
				canMove = false;
				hasJustMoved = true;
			}
			
		}
	}

	void MoveForward() {
		if(canMove){
			transform.position += Vector3.forward * 10;
			canMove = false;
			hasJustMoved = true;
		}
	}

	Vector2 CurPos() {
		return mr.CalculatePositionFromCoordinate(transform.position);
	}
	Vector2 MoveToPos(Vector2 pos) {
		return transform.position = mr.CalculatePositionFromCoordinate(pos);
	}
	
	void Timer() {
		if(!canMove) {
			if(cooldown <=0 && !hasJustMoved) {
				hasJustMoved = false;
				canMove = true;
			} else if (cooldown <=0){
				cooldown = movementCooldown;
				hasJustMoved = false;
			}
			if(cooldown > 0) {
				cooldown -= Time.deltaTime;
			}
		}	
	}
}

