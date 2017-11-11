using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationLibrary : MonoBehaviour {

	public List<TileTerrain> terrainTypes;
	public List<GameObject> treeTypes;
	public GameObject tilePrefab;


	public TileTerrain GetRandomTerrain(){
		int r = Random.Range(0,terrainTypes.Count);

		return terrainTypes[r];
	}

	public GameObject GetTree(int index){
		return treeTypes[index];
	}
}
