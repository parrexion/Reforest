using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActor : Actor {

	public Direction travelDirection;
	public float timeDelay;
	private float currentDelay;

    protected override void GetInput() {
		nextDirection = travelDirection;
		Vector2 nextPos = GetNextPositionFromDirection(nextDirection);
		if (mr.IsWalkable(nextPos))
			Destroy(gameObject);
		
    }

}
