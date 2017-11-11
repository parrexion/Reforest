using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTree : MonoBehaviour {

	public int hp;
	public int maxGrowthLevel;
	public float growthTime;

	public int currentGrowthLevel = 0;
	protected float currentGrowthTime;

	// Use this for initialization
<<<<<<< HEAD
	void Start () {
		Grow();
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

	protected abstract void DeGrow();

	public void TakeDamage() {
		currentGrowthLevel--;
		currentGrowthTime = 0;
		DeGrow();
	}

	protected void ChangeTreeType(int index){
		GameObject nextTree = Instantiate(MapGenerationLibrary.instance.GetTree(index));
		nextTree.transform.SetParent(transform.parent);
		nextTree.transform.localPosition = Vector3.zero;
		Destroy(gameObject);
	}
}
