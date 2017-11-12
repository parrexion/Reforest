using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour {


	public float fillUpTime = 1.25f;
	public float value;
	public float maxValue;
	public Slider bar;
	public Text t;
	public float timer;
	public float normalized;
	public float amountToFill;
	public bool visualizing;


	public bool isResourceGauge; 



	void Update() 
	{
		if(!isResourceGauge)
		return;


		if(visualizing && timer < fillUpTime) 
		{
			timer += Time.deltaTime;
			float change = (amountToFill/fillUpTime) * timer;
			
			value += change;
			amountToFill -= change; 

		}else 
		{
			visualizing = false;
		}	
			normalized = value / maxValue;			
			bar.normalizedValue = normalized;
			t.text = Mathf.RoundToInt(value) + " / " + maxValue;
	}

	public void Visualize(float f, float max = 0) 
	{
		if(!isResourceGauge) 
		{	
			VisualizeCooldown(f, max);
			return;
		}

			timer = 0;
			if(value != f) 
			{
				//Increase
				amountToFill = f - value;
				
				fillUpTime = Mathf.Abs((amountToFill / 10));

			}else 
			{
				Debug.Log("Didn't visualize");
				return;
			}
			visualizing = true;	
	}

	public void VisualizeCooldown(float f, float max) 
	{
		maxValue = max;
		value = f;

			normalized = Mathf.Clamp01(1 - f / max);			
			bar.normalizedValue = normalized;
	}

}
