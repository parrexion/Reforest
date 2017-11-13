using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	private Stats stats;
	private MapRepresentation mr;

	public int selectedBuilding = -1;
	public ToolTip tt;
	public Buttons buttons;

	[Header("Building costs")]
	public Costs[] costs;

	void Start () {
		stats = Stats.instance;
		mr = MapRepresentation.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) 
		{
			Select(Input.mousePosition);
			buttons.UpdateButtonSprites(selectedBuilding); 
		}
	}


	/// <summary>
	/// Get the cost for the building at the given index.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public Costs GetSelectedBuildingCosts(int selectedIndex){
		return (selectedIndex < 0) ? costs[selectedIndex] : new Costs(0,0);
	}

	/// <summary>
	/// Selectes a MapTile through raycast if a building is selected.
	/// </summary>
	/// <param name="mpos"></param>
	void Select(Vector2 mpos) 
	{	
		if(selectedBuilding == -1)
			return;
		
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000.0F)) {
			Debug.Log("Raycast Hit " + hit);
			MapTile tile = hit.transform.GetComponent<MapTile>();
			if (tile)
				TryBuild(tile);
			else {
				selectedBuilding = -1;
				Debug.Log("Raycast hit other object");
			}
		}
		else {
			selectedBuilding = -1;
			Debug.Log("Raycast missed");
		}
	}

	/// <summary>
	/// Checks if it possible to build the selected building on the tile and then builds it.
	/// </summary>
	/// <param name="tile"></param>
	void TryBuild(MapTile tile) 
	{
		Debug.Log("Trying to build at: " + tile.cord);

		if(tile.terrain.canHaveTrees && !tile.hasTree && mr.CheckIfTreeIsNearby(tile.cord)) 
		{
			if (selectedBuilding == 1 && tile.terrain.isSwamp) {
				selectedBuilding = 3;
			}
			Costs cost = GetSelectedBuildingCosts(selectedBuilding);
			if(stats.CanAfford(Stats.Resource.AQUA, cost.aquaticCost) && 
					stats.CanAfford(Stats.Resource.SUN, cost.solarCost))
			{
				Debug.Log("Building");
				Build(tile);
			}
			else {
				tt.ErrorMsg("Not enough resources");
			}
		}
		else if(!mr.CheckIfTreeIsNearby(tile.cord)) {
			tt.ErrorMsg("Too far away from forest");
		}
		else {
			tt.ErrorMsg("Build failed");
		}
	}

	/// <summary>
	/// Builds the selected building and pays the resources.
	/// </summary>
	/// <param name="tile"></param>
	void Build(MapTile tile) 
	{	
		tile.tree.ChangeTreeType(selectedBuilding);
		Costs cost = GetSelectedBuildingCosts(selectedBuilding);
		stats.DecreaseStat(Stats.Resource.AQUA, cost.aquaticCost);
		stats.DecreaseStat(Stats.Resource.SUN, cost.solarCost);
	}

	/// <summary>
	/// Selects the building and updates Stats.
	/// </summary>
	/// <param name="i"></param>
	public void SelectBuilding(int i) 
	{
		selectedBuilding = i;
		buttons.UpdateButtonSprites(selectedBuilding); 
	}
}
