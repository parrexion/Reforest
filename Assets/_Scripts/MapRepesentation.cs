using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRepesentation : MonoBehaviour {

	public List<MapTile> map;
	public Vector2 size;
	public Vector2 tileSize;


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
