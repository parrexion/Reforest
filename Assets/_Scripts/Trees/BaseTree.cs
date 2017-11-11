using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTree : MonoBehaviour {

	public int hp;
	public int maxGrowthLevel;
	public float growthTime;

	public int currentGrowthLevel;
	protected float currentGrowthTime;
	private Spawner s;


	// Use this for initialization
	protected void Start () {
		s = GetComponent<Spawner>();
	}
	
	// Update is called once per frame
	void Update () {
		currentGrowthTime += Time.deltaTime;
		if (currentGrowthTime <= growthTime) 
		{
			return;
		}if(currentGrowthLevel< maxGrowthLevel) 
		{
			Grow();
		}else 
		{
			s.Spawn();
			currentGrowthTime = 0;
		}
	}

	protected abstract void Grow();
}
