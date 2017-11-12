using System.Collections;		
 using System.Collections.Generic;		
 using UnityEngine;		
 		
 public class LumberActor : Actor {

	private Direction[] dirs = {Direction.EAST, Direction.NORTH, Direction.WEST, Direction.SOUTH};
	public Direction attackDirection;
 		
	protected override void GetInput() {
		if (isAttacking)
			nextDirection = attackDirection;
		else {
			nextDirection = GetDirectionTowardsMiddle();
			attackDirection = nextDirection;
		}
	}		
 		
	private Direction GetDirectionTowardsMiddle(){
		int xDiff = (int)currentCoordinate.x - (int)(mr.size.x/2);
		int yDiff = (int)currentCoordinate.y - (int)(mr.size.y/2);

		List<Direction> possibleDirs = new List<Direction>();
		if (xDiff > 0 && mr.IsWalkable(mr.GetNextPositionFromDirection(currentCoordinate, Direction.WEST)))
			possibleDirs.Add(Direction.WEST);
		if (yDiff > 0 && mr.IsWalkable(mr.GetNextPositionFromDirection(currentCoordinate, Direction.SOUTH)))
			possibleDirs.Add(Direction.SOUTH);
		if (xDiff < 0 && mr.IsWalkable(mr.GetNextPositionFromDirection(currentCoordinate, Direction.EAST)))
			possibleDirs.Add(Direction.EAST);
		if (yDiff < 0 && mr.IsWalkable(mr.GetNextPositionFromDirection(currentCoordinate, Direction.NORTH)))
			possibleDirs.Add(Direction.NORTH);

		if (possibleDirs.Count > 0) {
			int r = Random.Range(0,possibleDirs.Count);
			return possibleDirs[r];
		}
		int r2 = Random.Range(0,4);
		int d = Random.Range(0,2);
		Direction testDirection = dirs[r2];
		int c = 0;
		
		while (!mr.IsWalkable(mr.GetNextPositionFromDirection(currentCoordinate, testDirection)) && c < 4) {
			c++;
			testDirection = GetNextDirection(testDirection,d);
		}
		testDirection = (c>=4) ? Direction.NONE : testDirection;
		return testDirection;
	}

	private Direction GetNextDirection(Direction currDir, int rotation){
		switch(currDir)
		{
			case Direction.NORTH:
				return (rotation == 0) ? Direction.WEST : Direction.EAST;
			case Direction.WEST:
				return (rotation == 0) ? Direction.SOUTH : Direction.NORTH;
			case Direction.SOUTH:
				return (rotation == 0) ? Direction.EAST : Direction.WEST;
			case Direction.EAST:
				return (rotation == 0) ? Direction.NORTH : Direction.SOUTH;
			default:
				return Direction.NONE;
		}
	}
 }