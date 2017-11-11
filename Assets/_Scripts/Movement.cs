using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private MapRepesentation mr;
	public bool canMove = true;

	// Use this for initialization
	void Start () {
		mr = GameObject.Find("GenerateSampleGrid").GetComponent<MapRepesentation>();
		transform.position = mr.CalculatePositionFromCoordinate(new Vector2(5,5));
	}
	
	// Update is called once per frame
	void Update () {
		MoveActor();

		if(Input.GetKey(KeyCode.Space)) {
			//	
		}
	}

	void MoveActor() {
		
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += Vector3.right * 10;
			Debug.Log(CurPos());
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * 10;
			Debug.Log(CurPos());
		} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			transform.position += Vector3.forward * 10;
			Debug.Log(CurPos());
		} else if(Input.GetKeyDown(KeyCode.DownArrow)) {
			transform.position += Vector3.back * 10;
			Debug.Log(CurPos());
		}
		/*if (Input.GetAxis("Horizontal") > 0f) { //Höger
			if(mr.IsWalkable( new Vector2(CurPos().x + 10, CurPos().y )) && canMove) {
				canMove = false;
				Debug.Log("You can walk right");
				MoveToPos(new Vector2(CurPos().x + 10, CurPos().y));
				
			} else {
				Debug.Log("You cannot walk right");
			}
		} else if(Input.GetAxis("Horizontal") <  0f) { //Vänster
			if(mr.IsWalkable( new Vector2(CurPos().x - 10, CurPos().y ))) {
				Debug.Log("You can walk left");
			} else {
				Debug.Log("You cannot walk left");
			}
		} else if (Input.GetAxis("Vertical") > 0f) { //Upp
			Debug.Log("V Över 0");
		} else if(Input.GetAxis("Vertical") <  0f) { //Ner
			Debug.Log("V Under 0");
		}*/
	}

	Vector2 CurPos() {
		return mr.CalculatePositionFromCoordinate(transform.position);
	}
	Vector2 MoveToPos(Vector2 pos) {
		return transform.position = mr.CalculatePositionFromCoordinate(pos);
	}
}

