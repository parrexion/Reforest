using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowActor : MonoBehaviour {

	public Transform actor;
	public float height = 10;
	public float distance = 10;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(actor.position.x, actor.position.y + height, actor.position.z - distance);
		transform.LookAt(actor);
	}
}
