using System.Collections;		
 using System.Collections.Generic;		
 using UnityEngine;		
 		
 public class FireActor : Actor {		
 		
 	public Direction travelDirection;
 		
     protected override void GetInput() {		
 		nextDirection = travelDirection;		
 		Vector2 nextPos = mr.GetNextPositionFromDirection(currentCoordinate, nextDirection);		
 		if (!mr.IsWalkable(nextPos))		
 			Destroy(gameObject);	
     }		
 		
 }