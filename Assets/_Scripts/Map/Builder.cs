using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	public MapRepresentation mr;
	public ToolTip tt;
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
		Vector2 cord = new Vector2(tile.transform.position.x / mr.tileSize.x, tile.transform.position.z / mr.tileSize.y);

		Debug.Log(cord);

		MapTile mt = mr.getTile(cord);

		if(mt.canHasTrees && mt.tree != null) 
		{
			if (Stats.instance.selectedBuilding == 1 && mt.terrain.isSwamp)
				Stats.instance.selectedBuilding = 3;
			if(Stats.instance.costs[Stats.instance.selectedBuilding].aquaticCost <= Stats.instance.resources[0] &&
				Stats.instance.costs[Stats.instance.selectedBuilding].solarCost <= Stats.instance.resources[1] ) 
				{
					///BUILD 
					Debug.Log("Building");
					Build(mt, tile);
				}
				else  {
					tt.ErrorMsg("Not enough resources");
				}
		} else 
		{
			///Error
			Debug.Log("CanHasTrees = " + mt.canHasTrees.ToString() + "\n" + "HasTree = " + (mt.tree == null).ToString());
		}

	}

	void Build(MapTile mt, GameObject parent) 
	{	
		mt.tree.ChangeTreeType(Stats.instance.selectedBuilding);
		Stats.instance.DecreaseStat(0, Stats.instance.costs[Stats.instance.selectedBuilding].aquaticCost);
		Stats.instance.DecreaseStat(1, Stats.instance.costs[Stats.instance.selectedBuilding].solarCost);
	}

	public void SelectBuilding(int i) 
	{
		Stats.instance.selectedBuilding = i;
		buttons.UpdateButtonSprites(); 
	}

}
