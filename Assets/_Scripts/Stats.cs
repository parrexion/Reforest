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

	public enum Resource {AQUA = 0, SUN = 1}
	public int MaxSpawnedResources = 3;
	public int CurrentSpawnedResources = 0;

	public float[] currentRes;
	float[] maxResources;
	Gauge[] gauges;

	[Header ("Resource 0 (Left)")]
	float resource0 = 0;
	[SerializeField] private float resourceAquaMax;
	[SerializeField] private Gauge gaugeAqua;

	[Header ("Resource 1 (Right)")]
	float resource1 = 0;
	[SerializeField] private float resourceSunMax;
	[SerializeField] private Gauge gaugeSun;


    void Start () 
	{
		currentRes = new float[] { resource0, resource1 };
		maxResources = new float[] { resourceAquaMax, resourceSunMax };
		gauges = new Gauge[] { gaugeAqua, gaugeSun };
				
		gaugeAqua.maxValue = resourceAquaMax;
		gaugeSun.maxValue = resourceSunMax;	
	}
	
	// Update is called once per frame
	void Update ()
	
	{	

		if(Input.GetKeyDown("x")) 
		{
			//Resource 1
			IncreaseStat(Resource.AQUA, 25);
		}
		if(Input.GetKeyDown("z")) 
		{
			//Resource 1
			IncreaseStat(Resource.SUN, 25);
		}

		if(Input.GetKeyDown("v")) 
		{
			//Resource 1
			DecreaseStat(Resource.AQUA, 25);
		}
		if(Input.GetKeyDown("c")) 
		{
			//Resource 1
			DecreaseStat(Resource.SUN, 25);
		}


		if(Input.GetMouseButtonDown(0))
			PickUp(Input.mousePosition);
	}

	/// <summary>
	/// Check if we can afford the cost.
	/// </summary>
	/// <param name="cost"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public bool CanAfford(Resource type, float cost){
		return cost <= currentRes[(int)type];
	}

	/// <summary>
	/// Increases the resource with the given amount up to at most maximum.
	/// </summary>
	/// <param name="resourceID"></param>
	/// <param name="value"></param>
	public void IncreaseStat(Resource type, float value) 
	{
		currentRes[(int)type] = Mathf.Min(currentRes[(int)type] + value, maxResources[(int)type]);
		UpdateGauges();
	}

	/// <summary>
	/// Decreases the resource with the given amount down to at lowest 0.
	/// </summary>
	/// <param name="resourceID"></param>
	/// <param name="value"></param>
	public void DecreaseStat(Resource type, float value) 
	{
		currentRes[(int)type] = Mathf.Max(currentRes[(int)type] + value, 0);
		UpdateGauges();
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

	/// <summary>
	/// Updates the values for all gauges.
	/// </summary>
	void UpdateGauges() 
	{
		for (int i = 0; i < gauges.Length; i++) {
			gauges[i].UpdateRealValue(currentRes[i]);
		}
		Debug.Log("Updated gauges");
	}
}


