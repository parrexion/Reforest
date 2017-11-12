using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationLibrary : MonoBehaviour {

#region Singleton
public static MapGenerationLibrary instance;
	void Awake() {
		if (instance != null){
			Destroy(gameObject);
		}
		else {
			instance = this;
		}
	}
#endregion

	public List<TileTerrain> terrainTypes;
	public List<GameObject> treeTypes;
	public GameObject tilePrefab;
	public GameObject waterTree;
	public TileTerrain concreteTile;
	public TileTerrain worldTreeTile;
	public TileTerrain cityTile;


	public TileTerrain GetRandomTerrain(){
		int r = Random.Range(0,terrainTypes.Count);
		return terrainTypes[r];
	}

	public GameObject GetTree(int index){
		return treeTypes[index];
	}

	public TileTerrain GetSpecificTerrain(int x, int y) {

		//Create some water
		if (x == 5 && y == 8) return terrainTypes[1];
		if (x == 5 && y == 5) return terrainTypes[1];
		if (x == 8 && y == 8) return terrainTypes[1];
		if (x == 8 && y == 5) return terrainTypes[1];
		if (x == 15 && y == 17) return terrainTypes[1];
		if (x == 14 && y == 15) return terrainTypes[1];
		if (x == 12 && y == 15) return terrainTypes[1];
		if (x == 12 && y == 12) return terrainTypes[1];
		if (x == 14 && y == 7) return terrainTypes[1];
		if (x == 12 && y == 5) return terrainTypes[1];
		if (x == 16 && y == 9) return terrainTypes[1];
		if (x == 16 && y == 4) return terrainTypes[1];
		if (x == 6 && y == 14) return terrainTypes[1];
		if (x == 4 && y == 16) return terrainTypes[1];
		if (x == 8 && y == 13) return terrainTypes[1];
		if (x == 5 && y == 12) return terrainTypes[1];

		//Create some swamps
		if (x == 4 && y == 8) return terrainTypes[2];
		if (x == 6 && y == 8) return terrainTypes[2];
		if (x == 5 && y == 7) return terrainTypes[2];
		if (x == 5 && y == 9) return terrainTypes[2];
		if (x == 4 && y == 5) return terrainTypes[2];
		if (x == 6 && y == 5) return terrainTypes[2];
		if (x == 5 && y == 4) return terrainTypes[2];
		if (x == 5 && y == 6) return terrainTypes[2];
		if (x == 7 && y == 8) return terrainTypes[2];
		if (x == 9 && y == 8) return terrainTypes[2];
		if (x == 8 && y == 7) return terrainTypes[2];
		if (x == 8 && y == 9) return terrainTypes[2];
		if (x == 7 && y == 5) return terrainTypes[2];
		if (x == 9 && y == 5) return terrainTypes[2];
		if (x == 8 && y == 4) return terrainTypes[2];
		if (x == 8 && y == 6) return terrainTypes[2];
		
		if (x == 14 && y == 17) return terrainTypes[2];
		if (x == 16 && y == 17) return terrainTypes[2];
		if (x == 15 && y == 16) return terrainTypes[2];
		if (x == 15 && y == 18) return terrainTypes[2];
		if (x == 13 && y == 15) return terrainTypes[2];
		if (x == 15 && y == 15) return terrainTypes[2];
		if (x == 14 && y == 14) return terrainTypes[2];
		if (x == 14 && y == 16) return terrainTypes[2];
		if (x == 11 && y == 15) return terrainTypes[2];
		if (x == 13 && y == 15) return terrainTypes[2];
		if (x == 12 && y == 14) return terrainTypes[2];
		if (x == 12 && y == 16) return terrainTypes[2];
		if (x == 11 && y == 12) return terrainTypes[2];
		if (x == 13 && y == 12) return terrainTypes[2];
		if (x == 12 && y == 11) return terrainTypes[2];
		if (x == 12 && y == 13) return terrainTypes[2];

		if (x == 13 && y == 7) return terrainTypes[2];
		if (x == 15 && y == 7) return terrainTypes[2];
		if (x == 14 && y == 6) return terrainTypes[2];
		if (x == 14 && y == 8) return terrainTypes[2];
		if (x == 11 && y == 5) return terrainTypes[2];
		if (x == 13 && y == 5) return terrainTypes[2];
		if (x == 12 && y == 4) return terrainTypes[2];
		if (x == 12 && y == 6) return terrainTypes[2];
		if (x == 15 && y == 9) return terrainTypes[2];
		if (x == 17 && y == 9) return terrainTypes[2];
		if (x == 16 && y == 10) return terrainTypes[2];
		if (x == 16 && y == 8) return terrainTypes[2];
		if (x == 15 && y == 4) return terrainTypes[2];
		if (x == 17 && y == 4) return terrainTypes[2];
		if (x == 16 && y == 3) return terrainTypes[2];
		if (x == 16 && y == 5) return terrainTypes[2];

		if (x == 5 && y == 14) return terrainTypes[2];
		if (x == 7 && y == 14) return terrainTypes[2];
		if (x == 6 && y == 13) return terrainTypes[2];
		if (x == 6 && y == 15) return terrainTypes[2];
		if (x == 3 && y == 16) return terrainTypes[2];
		if (x == 5 && y == 16) return terrainTypes[2];
		if (x == 4 && y == 15) return terrainTypes[2];
		if (x == 4 && y == 17) return terrainTypes[2];
		if (x == 7 && y == 13) return terrainTypes[2];
		if (x == 9 && y == 13) return terrainTypes[2];
		if (x == 8 && y == 12) return terrainTypes[2];
		if (x == 8 && y == 14) return terrainTypes[2];
		if (x == 4 && y == 12) return terrainTypes[2];
		if (x == 6 && y == 12) return terrainTypes[2];
		if (x == 5 && y == 11) return terrainTypes[2];
		if (x == 5 && y == 13) return terrainTypes[2];

		return terrainTypes[0];
	}

	public GameObject GetSpecificTree(int x, int y){
		if (x == 9 && y == 10) return treeTypes[1];
		if (x == 11 && y == 10) return treeTypes[1];
		if (x == 10 && y == 9) return treeTypes[1];
		if (x == 10 && y == 11) return treeTypes[1];
		if (x == 11 && y == 11) return treeTypes[1];
		if (x == 9 && y == 11) return treeTypes[1];
		if (x == 9 && y == 9) return treeTypes[1];
		if (x == 11 && y == 9) return treeTypes[1];
		if (x == 8 && y == 10) return treeTypes[1];
		if (x == 12 && y == 10) return treeTypes[1];
		if (x == 10 && y == 8) return treeTypes[1];
		if (x == 10 && y == 12) return treeTypes[1];

		if (x == 8 && y == 9) return treeTypes[3];
		if (x == 12 && y == 9) return treeTypes[1];
		if (x == 9 && y == 8) return treeTypes[3];
		if (x == 9 && y == 12) return treeTypes[1];
		if (x == 8 && y == 11) return treeTypes[1];
		if (x == 12 && y == 11) return treeTypes[3];
		if (x == 11 && y == 8) return treeTypes[1];
		if (x == 11 && y == 12) return treeTypes[3];
		return treeTypes[0];
	}	
}
