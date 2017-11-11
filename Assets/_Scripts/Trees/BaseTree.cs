using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTree : MonoBehaviour {

	public int hp;
	public int maxGrowthLevel;
	public float growthTime;

	public int currentGrowthLevel;
	protected float currentGrowthTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentGrowthLevel >= maxGrowthLevel)
			return;

		currentGrowthTime += Time.deltaTime;
		if (currentGrowthTime >= growthTime) {
			Grow();
		}
	}

	protected abstract void Grow();
}
