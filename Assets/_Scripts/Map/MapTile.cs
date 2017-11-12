using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTile {

	public BaseTree tree;
	public TileTerrain terrain;
	public List<Actor> actors = new List<Actor>();
	public List<Pickups> pickups;
	public bool canHasTrees;
	public Vector2 cord;


	public int RegisterAtTile(Actor act){
		if (actors.Count >= 4){
			return -1;
		}
		
		actors.Add(act);
		return actors.Count-1;
	}

	public void UnregisterAtTile(int id) {
		for (int i = 0; i < actors.Count; i++)
		{
			if (actors[i].id == id) {
				actors.RemoveAt(i);
				return;
			}
		}
	}
}
