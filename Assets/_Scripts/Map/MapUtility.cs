using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapUtility {

	public static readonly Vector2 tileSize = new Vector2(10,10);
	public static readonly Vector2 mapSize = new Vector2(21,21);
	public static readonly float spawnHeight = 5f;
	public static readonly int maxEnemiesTogether = 4;
	public static readonly Direction[] allDirections = {Direction.EAST, Direction.NORTH, Direction.WEST, Direction.SOUTH};

	/// <summary>
	/// Returns the next coordinate given the direction to move in.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <param name="nextDir"></param>
	/// <returns></returns>
	public static Vector2 GetNextPositionFromDirection(Vector2 coordinate, Direction nextDir) {
		switch (nextDir) {
			case Direction.NORTH:
				return new Vector2(coordinate.x, coordinate.y+1);
			case Direction.WEST:
				return new Vector2(coordinate.x-1, coordinate.y);
			case Direction.EAST:
				return new Vector2(coordinate.x+1, coordinate.y);
			case Direction.SOUTH:
				return new Vector2(coordinate.x, coordinate.y-1);
			default:		
 				return coordinate;
		}
	}

	/// <summary>
	/// Returns the world position for the given coordinate.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <param name="tilePosition"></param>
	/// <returns></returns>
	public static Vector3 ConvertCoordinateToWorldPosition(Vector2 coordinate, int tilePosition){
		float xOff = 0f;
		float zOff = 0f; 
		if (tilePosition != -1){
			xOff = (tilePosition > 1) ? 2.5f : -2.5f;
			zOff = (tilePosition % 2 == 0) ? 2.5f : -2.5f; 
		}
		return new Vector3(coordinate.x*tileSize.x+xOff, 0.5f,coordinate.y*tileSize.y+zOff);
	}

	/// <summary>
	/// Returns the direction pointing away from the edge of the map.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <returns></returns>
	public static Direction FaceFromEdge(Vector2 coordinate){
		if (coordinate.x == 1)
			return Direction.EAST;
		if (coordinate.y == 1)
			return Direction.NORTH;
		if (coordinate.x == mapSize.x-2)
			return Direction.WEST;
		if (coordinate.y == mapSize.y-2)
			return Direction.SOUTH;

		return Direction.NONE;
	}

	/// <summary>
	/// Converts the current position of the tile to map coordinates.
	/// </summary>
	/// <param name="position"></param>
	/// <returns></returns>
	public static Vector2 ConvertWorldPositionToCoordinate(Vector3 position) {
		return new Vector2(position.x / tileSize.x, position.z / tileSize.y);
	}


	public static bool IsOutOfBounds(Vector2 coordinate) {
		return (coordinate.x < 0 || coordinate.y < 0 || coordinate.x >= mapSize.x || coordinate.y >= mapSize.y);
	}
}
