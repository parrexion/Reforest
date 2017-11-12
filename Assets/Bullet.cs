using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 3;
	public float timeToDestroy = 2;


	// Use this for initialization
	void Start () {
		//this.transform.rotation = parent
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
		timeToDestroy -= Time.deltaTime;

		if(timeToDestroy <= 0) {
			Debug.Log("Destroy Bullet");
			Destroy(gameObject);
		}
	}
}
