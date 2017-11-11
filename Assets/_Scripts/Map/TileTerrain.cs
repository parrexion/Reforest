using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Terrain")]
public class TileTerrain : ScriptableObject {

	public Material material;
	public bool isWalkable;
	public bool canHasTrees;
	public bool isWater;
}
