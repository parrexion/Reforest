using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

	public Vector2 sizeOfGrid = new Vector2(10,10);
	public int sizeOfPlane = 10;
	public int offset = 10;
	public GameObject plane;
	private MapRepesentation mr;
	private Transform parent;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MapRepesentation>();
		mr.size = sizeOfGrid;
		parent = this.transform;

		for(int i=1; i<sizeOfGrid.x-1; i++) {
			for(int b=1; b<sizeOfGrid.y-1; b++) {
				Instantiate(plane, new Vector3(0,0,0) + new Vector3(sizeOfPlane * i + offset, 0, sizeOfPlane * b + offset), transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
