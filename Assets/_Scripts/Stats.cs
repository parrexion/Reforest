﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

#region Singleton

    public static Stats instance;

    protected void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;

		costs = new Costs[] { new Costs(0, 0), new Costs(25, 25), new Costs(0, 40), new Costs(25, 25)};
			
        }
    }
#endregion


	public Costs[] costs;


	public float[] resources;
	float[] resourcesMax;
	Gauge[] resourcesGauge;

	[Header("Resource 0 (Left)")]

	public float resource0Max;
	public Gauge resource0Gauge;

	[Header("Resource 1 (Right)")]
	public float resource1Max;	
	public Gauge resource1Gauge;	

	float resource0 = 0;
	float resource1 = 0; 
	// Use this for initialization

	public int selectedBuilding;


    void Start () 
	{
		resources = new float[] { resource0, resource1 };
		resourcesMax = new float[] { resource0Max, resource1Max };
		resourcesGauge = new Gauge[] { resource0Gauge, resource1Gauge };
				
		resource0Gauge.maxValue = resource0Max;
		resource1Gauge.maxValue = resource1Max;	

		selectedBuilding = -1;

	}
	
	// Update is called once per frame
	void Update ()
	
	{	

		if(Input.GetKeyDown("x")) 
		{
			//Resource 1
			IncreaseStat(0, 25);
		}
		if(Input.GetKeyDown("z")) 
		{
			//Resource 1
			IncreaseStat(1, 25);
		}

		if(Input.GetKeyDown("v")) 
		{
			//Resource 1
			DecreaseStat(0, 25);
		}
		if(Input.GetKeyDown("c")) 
		{
			//Resource 1
			DecreaseStat(1, 25);
		}


		if(Input.GetMouseButtonDown(0))
			PickUp(Input.mousePosition);

	}

	public void IncreaseStat(int resourceID, float value) 
	{
		if(resources[resourceID] + value <= resourcesMax[resourceID] )
		{
			resources[resourceID] += value;
			// resourcesGauge[resourceID].value = resources[resourceID];
		}
	
		VisualizeGauges();
	}

	public void DecreaseStat(int resourceID, float value) 
	{
		if (resources[resourceID] - value >= 0) 
		{
			resources[resourceID] -= value;			
			// resourcesGauge[resourceID].value = resources[resourceID];
			VisualizeGauges();
		}
	}

	void PickUp(Vector2 mpos) 
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000.0F) && hit.collider.tag == "PickUp") 
		{
			hit.collider.gameObject.GetComponent<EnergySphere>().Collect();
			Debug.Log(hit.collider);
		}


	}

	void VisualizeGauges() 
	{
		int i = 0;
		foreach(Gauge g in resourcesGauge) 
		{ 
			g.Visualize(resources[i]);
			i++; 	
		}

	}

}


