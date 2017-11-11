using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	public MapRepresentation mr;

	public Buttons buttons;

	bool building;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) 
		{
			Select(Input.mousePosition);
			buttons.UpdateButtonSprites(); 
			
		}
	}


	void Select(Vector2 mpos) 
	{	
		if(Stats.instance.selectedBuilding == -1)
			return; 
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0F) && hit.collider.tag == "Tile") 
		{
			Debug.Log("Raycast Hit " + hit);
			TryBuild(hit.collider.gameObject);
		}else 
		{
			Stats.instance.selectedBuilding = -1;
			Debug.Log("Raycast missed");
		}
	}

	void TryBuild(GameObject tile) 
	{
		Vector2 cord = new Vector2(tile.transform.position.z / mr.tileSize.x, tile.transform.position.x / mr.tileSize.y);

		Debug.Log(cord);

		MapTile mt = mr.getTile(cord);

		if(mt.canHasTrees && mt.tree != null) 
		{
			///BUILD 
			Debug.Log("Building");
			Build(mt, tile);

		} else 
		{
			///Error
			Debug.Log("CanHasTrees = " + mt.canHasTrees.ToString() + "\n" + "HasTree = " + (mt.tree == null).ToString());
		}

	}

	void Build(MapTile mt, GameObject parent) 
	{
		// GameObject go = Instantiate(buildings[selectedBuilding]) as GameObject;
		// mt.tree = go.GetComponent<BaseTree>();
		// go.transform.parent = parent.transform;
		// selectedBuilding = -1;
		
		mt.tree.ChangeTreeType(Stats.instance.selectedBuilding);
	}

	public void SelectBuilding(int i) 
	{
		Stats.instance.selectedBuilding = i;
		buttons.UpdateButtonSprites(); 
		

	}

}
