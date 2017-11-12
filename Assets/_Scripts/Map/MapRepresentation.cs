﻿using System.Collections;
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
				bool isConcrete = false;
				//Add spawn point
				if (i == 0 ^ j == 0 ^ i == size.x-1 ^ j == size.y-1) {
					eSpawn.spawnLocations.Add(new Vector2(i, j));
					isConcrete = true;
				}

				//Generate tile information
				tile = new MapTile();
				tile.terrain = (isConcrete) ? mapLib.concreteTile : mapLib.GetRandomTerrain();
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
					int r = Random.Range(0,3);
					GameObject tree = (r==0) ? Instantiate(mapLib.GetTree(1)) : Instantiate(mapLib.GetTree(0));
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = Vector3.zero;
					tile.tree = tree.GetComponent<BaseTree>();
				} else if (tile.terrain.isWater){
					tileObj.transform.position -= new Vector3(0,0.15f,0);
					tileObj.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
					GameObject tree = Instantiate(mapLib.waterTree);
					tree.transform.SetParent(tileObj.transform);
					tree.transform.localPosition = new Vector3(0,0.15f,0);
					tile.tree = tree.GetComponent<BaseTree>();
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
		return tile.terrain.isWalkable;
	}

	public bool HasTrees(Vector2 position) {
		MapTile tile = getTile(position);
		if (tile.tree == null)
			return false;
		return (tile.tree.currentGrowthLevel > 0);
	}

	public Vector3 CalculatePositionFromCoordinate(Vector2 position){
		return new Vector3(position.x*tileSize.x, 0.5f,position.y*tileSize.y);
	}

	
	private int convertToIndex(Vector2 position){
		return (int)position.y*(int)size.x + (int)position.x;
	}
}
