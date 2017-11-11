using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTree : MonoBehaviour {

	public int hp;
	public int maxGrowthLevel;
	public float growthTime;

	public int currentGrowthLevel = 0;
	protected float currentGrowthTime;
	private Spawner s;


	protected void Start () {
		s = GetComponent<Spawner>();
		Grow();
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
