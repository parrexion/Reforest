using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRepresentation : MonoBehaviour {

#region Singleton
    public static MapRepresentation instance;

    protected void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
#endregion

	private List<MapTile> map = new List<MapTile>();
	public List<Vector2> spawnLocations = new List<Vector2>();
	private MapGenerationLibrary mapLib;


	void Start() {
		mapLib = GetComponent<MapGenerationLibrary>();
		GenerateMap();
	}

	private void GenerateMap() {
		MapTile tile;
		for (int j = 0; j < MapUtility.mapSize.x; j++) {
			for (int i = 0; i < MapUtility.mapSize.y; i++) {
				bool isCity = false;
				bool isConcrete = false;
				//Add City border
				if (i == 0 || j == 0 || i == MapUtility.mapSize.x-1 || j == MapUtility.mapSize.y-1) {
					if (i == 0 ^ j == 0 ^ i == MapUtility.mapSize.x-1 ^ j == MapUtility.mapSize.y-1) {
						bool spawnCity = ((i+j) % 3 != 0);
						isCity = spawnCity;
						isConcrete = !spawnCity;
					}
					else {
						isConcrete = true;
					}
				}
				else if (i == 1 || j == 1 || i == MapUtility.mapSize.x-2 || j == MapUtility.mapSize.y-2) {
					isConcrete = true;
					if (i == 1 ^ j == 1 ^ i == MapUtility.mapSize.x-2 ^ j == MapUtility.mapSize.y-2)
						spawnLocations.Add(new Vector2(i, j));
				}
				
				//Generate visual tile
				GameObject tileObj = Instantiate(mapLib.tilePrefab);
				tileObj.transform.position = MapUtility.ConvertCoordinateToWorldPosition(new Vector2(i,j),-1);
				tileObj.transform.localRotation = Quaternion.identity;
				tileObj.transform.parent = this.transform;

				//Generate tile information
				tile = tileObj.GetComponent<MapTile>();
				tile.cord = new Vector2(i, j);
				tile.terrain = ScriptableObject.Instantiate((isCity) ? mapLib.terrainTypes[5] : 
								(isConcrete) ? mapLib.terrainTypes[3] : mapLib.GetSpecificTerrain(i,j));
				tileObj.GetComponent<MeshRenderer>().material = tile.terrain.material;
				map.Add(tile);

				//Create the special tree trees
				if (i == (int)MapUtility.mapSize.x/2 && j == (int)MapUtility.mapSize.y/2) {
					//World tree in the middle
					GameObject tree = Instantiate(mapLib.GetTree(5));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
					tile.terrain = mapLib.terrainTypes[4];
					tileObj.GetComponent<MeshRenderer>().material = tile.terrain.material;
				}
				else if (tile.terrain.canHaveTrees){
					//Basic or no tree
					GameObject tree = Instantiate(mapLib.GetSpecificTree(i,j));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
					tile.tree.currentGrowthLevel = tile.tree.maxGrowthLevel;
				} else if (tile.terrain.isWater){
					//Water wave
					tileObj.transform.position -= new Vector3(0,0.15f,0);
					tileObj.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
					GameObject tree = Instantiate(mapLib.waterTree);
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = new Vector3(0,0.15f,0);
					tile.tree = tree.GetComponent<BaseTree>();
				} else if (tile.terrain.isCity){
					//City trees
					GameObject tree = Instantiate(mapLib.GetTree(4));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
					if (i == 0)
						tree.transform.localRotation = Quaternion.Euler(0,0,0);
					if (j == 0)
						tree.transform.localRotation = Quaternion.Euler(0,90,0);
					if (i == MapUtility.mapSize.x)
						tree.transform.localRotation = Quaternion.Euler(0,180,0);
					if (j == MapUtility.mapSize.y)
						tree.transform.localRotation = Quaternion.Euler(0,270,0);
				}
			}
		}
	}

	public MapTile getTile(Vector2 coordinate) {
		return map[convertToIndex(coordinate)];
	}

	private int convertToIndex(Vector2 position){
		return (int)position.y*(int)MapUtility.mapSize.x + (int)position.x;
	}

	/// <summary>
	/// Checks if the tile can be walked on or attacked by enemies.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <param name="actor"></param>
	/// <returns></returns>
	public bool IsWalkable(Vector2 coordinate, Actor actor){
		//Out of bounds
		if (MapUtility.IsOutOfBounds(coordinate))
			return false;

		MapTile tile = getTile(coordinate);
		return tile.terrain.isWalkable || (actor.isEnemy && tile.terrain.isAttackable);
	}

	/// <summary>
	/// Checks if a tile has any trees planted at the moment.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <returns></returns>
	public bool HasTrees(Vector2 coordinate) {
		MapTile tile = getTile(coordinate);
		if (tile.tree == null)
			return false;
		return (tile.tree.currentGrowthLevel > 0);
	}

	/// <summary>
	/// Checks if one of the tiles beside the coordinate has a tree.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <returns></returns>
	public bool CheckIfTreeIsNearby(Vector2 coordinate){
		if (HasTrees(MapUtility.GetNextPositionFromDirection(coordinate, Direction.NORTH)))
			return true;
		if (HasTrees(MapUtility.GetNextPositionFromDirection(coordinate, Direction.WEST)))
			return true;
		if (HasTrees(MapUtility.GetNextPositionFromDirection(coordinate, Direction.SOUTH)))
			return true;
		if (HasTrees(MapUtility.GetNextPositionFromDirection(coordinate, Direction.EAST)))
			return true;

		return false;
	}
}
