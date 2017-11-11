using System.Collections;
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
        }
    }
    #endregion


	float[] resources;
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
		
	}
	
	// Update is called once per frame
	void Update ()
	
	{	
		if(Input.GetKeyDown("x")) 
		{
			//Resource 1
			IncreaseStat(0, 5);
		}
		if(Input.GetKeyDown("z")) 
		{
			//Resource 1
			IncreaseStat(1, 5);
		}

		if(Input.GetKeyDown("v")) 
		{
			//Resource 1
			DecreaseStat(0, 5);
		}
		if(Input.GetKeyDown("c")) 
		{
			//Resource 1
			DecreaseStat(1, 5);
		}

		if(Input.GetMouseButtonDown(0))
			PickUp(Input.mousePosition);


	
		//Update gauges
		resource0Gauge.Visualize();
		resource1Gauge.Visualize();			
	}

	public void IncreaseStat(int resourceID, float value) 
	{
		if(resources[resourceID] + value <= resourcesMax[resourceID] )
		{
			resources[resourceID] += value;
			resourcesGauge[resourceID].value = resources[resourceID];
		}
	}

	public void DecreaseStat(int resourceID, float value) 
	{
		if (resources[resourceID] < value)
			return;

			resources[resourceID] -= value;			
			resourcesGauge[resourceID].value = resources[resourceID];
	}


	void PickUp(Vector2 mpos) 
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0F) && hit.collider.tag == "PickUp") 
		{
			hit.collider.gameObject.GetComponent<EnergySphere>().Collect();
			Debug.Log(hit.collider);
		}


	}

}
