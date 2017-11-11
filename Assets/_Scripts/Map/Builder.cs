using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	Transform selectedTile;

	public int selectedBuilding;

	public MapRepresentation mr;


	public List<GameObject> buildings;

	bool building;
	// Use this for initialization
	void Start () {

		buildings = new List<GameObject>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) 
		{
			Select(Input.mousePosition);

		}
	}


	void Select(Vector2 mpos) 
	{
		if(!building) 
			return;
		
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0F) && hit.collider.tag == "Tile") 
		{
			selectedTile = hit.collider.transform;
		}
	}

	void TryBuild(GameObject tile) 
	{
		MapTile mt = mr.getTile(new Vector2(tile.transform.position.x, tile.transform.position.y));

		if(mt.canHasTrees && mt.tree == null) 
		{
			///BUILD 
			Build(mt);

		} else 
		{
			///Error
			

		}

	}

	void Build(MapTile mt) 
	{
		GameObject go = Instantiate(buildings[selectedBuilding]) as GameObject;
		mt.tree = go.GetComponent<BaseTree>();
		go.transform.parent = selectedTile;

	}

	public void SelectBuilding(int i) 
	{
		selectedBuilding = i;
	}

}
