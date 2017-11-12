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

	[SerializeField]
	private List<MapTile> map;
	private MapGenerationLibrary mapLib;

	public Vector2 size;
	public Vector2 tileSize;
	public int spawnHeight = 3;

	public void setSpawnHeight(int val){
		spawnHeight = val;
	}

	void Start() {
		mapLib = GetComponent<MapGenerationLibrary>();
		GenerateMap();
	}

	private void GenerateMap() {
		MapTile tile;
		EnemySpawner eSpawn = EnemySpawner.instance;
		for (int j = 0; j < size.x; j++) {
			for (int i = 0; i < size.y; i++) {
				bool isCity = false;
				bool isConcrete = false;
				//Add City border
				if (i == 0 || j == 0 || i == size.x-1 || j == size.y-1) {
					if (i == 0 ^ j == 0 ^ i == size.x-1 ^ j == size.y-1) {
						bool spawnCity = ((i+j) % 3 != 0);
						isCity = spawnCity;
						isConcrete = !spawnCity;
					}
					else {
						isConcrete = true;
					}
				}
				else if (i == 1 || j == 1 || i == size.x-2 || j == size.y-2) {
					isConcrete = true;
					if (i == 1 ^ j == 1 ^ i == size.x-2 ^ j == size.y-2)
						eSpawn.spawnLocations.Add(new Vector2(i, j));
				}

				//Generate tile information
				tile = new MapTile();
				tile.terrain = ScriptableObject.Instantiate((isCity) ? mapLib.cityTile : (isConcrete) ? mapLib.concreteTile : mapLib.GetSpecificTerrain(i,j));
				tile.canHasTrees = tile.terrain.canHasTrees;
				map.Add(tile);
				tile.cord = new Vector2(i, j);
				
				//Generate visual tile
				GameObject tileObj = Instantiate(mapLib.tilePrefab);
				tileObj.transform.position = new Vector3(tileSize.x * i, 0, tileSize.y * j);
				tileObj.transform.localRotation = Quaternion.identity;
				tileObj.GetComponent<MeshRenderer>().material = tile.terrain.material;
				tileObj.transform.parent = this.transform;

				//Add some trees for now
				if (i == (int)(size.x/2) && j == (int)(size.y/2)) {
					GameObject tree = Instantiate(mapLib.GetTree(3));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
					tileObj.GetComponent<MeshRenderer>().material = mapLib.worldTreeTile.material;
				}
				else if (tile.canHasTrees){
					GameObject tree = Instantiate(mapLib.GetSpecificTree(i,j));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
					tile.tree.currentGrowthLevel = tile.tree.maxGrowthLevel;
				} else if (tile.terrain.isWater){
					tileObj.transform.position -= new Vector3(0,0.15f,0);
					tileObj.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
					GameObject tree = Instantiate(mapLib.waterTree);
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = new Vector3(0,0.15f,0);
					tile.tree = tree.GetComponent<BaseTree>();
				} else if (tile.terrain.isCity){
					GameObject tree = Instantiate(mapLib.GetTree(4));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
					if (i == 0)
						tree.transform.localRotation = Quaternion.Euler(0,0,0);
					if (j == 0)
						tree.transform.localRotation = Quaternion.Euler(0,90,0);
					if (i == size.x)
						tree.transform.localRotation = Quaternion.Euler(0,180,0);
					if (j == size.y)
						tree.transform.localRotation = Quaternion.Euler(0,270,0);
				}
			}
		}
	}

	public MapTile getTile(Vector2 position) {
		return map[convertToIndex(position)];
	}

	public bool IsWalkable(Vector2 position){
		//Out of bounds
		if (position.x < 0 || position.y < 0 || position.x >= size.x || position.y >= size.y)
			return false;

		MapTile tile = getTile(position);
		return tile.terrain.isWalkable && !tile.terrain.isWater;
	}

	public bool HasTrees(Vector2 position) {
		MapTile tile = getTile(position);
		if (tile.tree == null)
			return false;
		return (tile.tree.currentGrowthLevel > 0);
	}

	public Vector3 CalculatePositionFromCoordinate(Vector2 position, int tilePosition){
		float xOff = 0f;
		float zOff = 0f; 
		if (tilePosition != -1){
			xOff = (tilePosition > 1) ? 2.5f : -2.5f;
			zOff = (tilePosition % 2 == 0) ? 2.5f : -2.5f; 
		}
		return new Vector3(position.x*tileSize.x+xOff, 0.5f,position.y*tileSize.y+zOff);
	}

	
	private int convertToIndex(Vector2 position){
		return (int)position.y*(int)size.x + (int)position.x;
	}
}
