using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private MapRepesentation mr;

	// Use this for initialization
	void Start () {
		mr = GameObject.Find("GenerateSampleGrid").GetComponent<MapRepesentation>();
		CurPos();
	}
	
	// Update is called once per frame
	void Update () {
		MoveActor();

		

		if(Input.GetKey(KeyCode.Space)) {
			//Debug.Log(mr.CalculatePositionFromCoordinate());
			
			
		}
	}

	void MoveActor() {
		if (Input.GetAxis("Horizontal") > 0f) { //Höger
			if(mr.IsWalkable())
			Debug.Log("H Över 0");
		} else if(Input.GetAxis("Horizontal") <  0f) { //Vänster
			Debug.Log("H Under 0");
		} else if (Input.GetAxis("Vertical") > 0f) { //Upp
			Debug.Log("V Över 0");
		} else if(Input.GetAxis("Vertical") <  0f) { //Ner
			Debug.Log("V Under 0");
		}
	}

	Vector2 CurPos() {
		return transform.position = mr.CalculatePositionFromCoordinate(new Vector2(0,0));
	}
}
