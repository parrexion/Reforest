using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationLibrary : MonoBehaviour {

	public List<TileTerrain> terrainTypes;
	public List<BaseTree> treeTypes;
	public GameObject tilePrefab;


	public TileTerrain GetRandomTerrain(){
		int r = Random.Range(0,terrainTypes.Count);

		return terrainTypes[r];
	}

	public BaseTree GetTree(){
		return treeTypes[0];
	}
}
