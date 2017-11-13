using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTile : MonoBehaviour{

	public Vector2 cord;
	public TileTerrain terrain;
	public BaseTree tree;
	public List<Actor> actors = new List<Actor>();
	public bool hasTree { get { return (tree != null && tree.currentGrowthLevel > 0); }}


	public int RegisterAtTile(Actor act){
		if (actors.Count >= MapUtility.maxEnemiesTogether){
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
