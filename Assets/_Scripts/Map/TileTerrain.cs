using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Terrain")]
public class TileTerrain : ScriptableObject {

	public Material material;
	public bool isWalkable;
	public bool canHaveTrees;
	public bool isAttackable;
	public bool isWater;
	public bool isSwamp;
	public bool isCity;
}
