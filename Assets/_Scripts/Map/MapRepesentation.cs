using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRepesentation : MonoBehaviour {

	[SerializeField]
	private List<MapTile> map;
	private MapGenerationLibrary mapLib;

	public Vector2 size;
	public Vector2 tileSize;


	void Start() {
		mapLib = GetComponent<MapGenerationLibrary>();
		GenerateMap();
	}

	private void GenerateMap() {
		MapTile tile;
		for (int i = 0; i < size.x; i++) {
			for (int j = 0; j < size.y; j++) {
				//Generate tile information
				tile = new MapTile();
				tile.terrain = mapLib.GetRandomTerrain();
				map.Add(tile);
				
				//Generate visual tile
				GameObject tileObj = Instantiate(mapLib.tilePrefab);
				tileObj.transform.position = new Vector3(tileSize.x * i, 0, tileSize.y * j);
				tileObj.transform.localRotation = Quaternion.identity;
				tileObj.GetComponent<MeshRenderer>().material = tile.terrain.material;
				tileObj.transform.parent = this.transform;
			}
		}
	}

	public MapTile getTile(Vector2 position) {
		return map[convertToIndex(position)];
	}

	public bool IsWalkable(Vector2 position){
		return true;
	}

	public Vector3 CalculatePositionFromCoordinate(Vector2 position){
		return new Vector3(position.x*tileSize.x, position.y*tileSize.y,0);
	}

	
	private int convertToIndex(Vector2 position){
		return (int)position.y*(int)size.x + (int)position.x;
	}
}
