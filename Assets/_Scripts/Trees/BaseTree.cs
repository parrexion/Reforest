using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTree : MonoBehaviour {

	public int maxGrowthLevel;
	public float growthTime;
	public int currentGrowthLevel = 0;
	public bool rotateTrees = true;
	
	protected float currentGrowthTime;
	private Spawner s;


	protected virtual void Start () {
		s = GetComponent<Spawner>();
		if (rotateTrees)
			RandomizeTrees();
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
			if(s != null)
				s.Spawn();
			currentGrowthTime = 0;
		}
	}

	void RandomizeTrees() {
		int r = Random.Range(0,4);
		transform.localRotation = Quaternion.Euler(0,90f*r,0);
		Transform[] trees = GetComponentsInChildren<Transform>();
		float diff;
		Vector3 oldScale;
		for (int i = 1; i < trees.Length; i++) {
			diff = Random.Range(0.85f,1.15f);
			oldScale = trees[i].localScale;
			trees[i].localScale = new Vector3(oldScale.x*diff,oldScale.y*diff,oldScale.z*diff);
			trees[i].localRotation = Quaternion.Euler(0,Random.Range(0,360),0);
		}
	}

	protected abstract void Grow();

	protected abstract void DeGrow();

	public void TakeDamage() {
		if (currentGrowthLevel <= 0)
			return;
			
		currentGrowthLevel--;
		currentGrowthTime = 0;
		DeGrow();
	}

	public void ChangeTreeType(int index){
		GameObject nextTree = Instantiate(MapGenerationLibrary.instance.GetTree(index));
		nextTree.transform.SetParent(transform.parent);
		nextTree.transform.localPosition = Vector3.zero;
		Destroy(gameObject);
	}
}
