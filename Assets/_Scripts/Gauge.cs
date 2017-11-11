using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour {


	public float fillUpTime = 1.25f;
	public float value;
	public float maxValue;
	public Slider bar;
	public float width = 250;
	public Text t;
	private float timer;

	public float amountToFill;
	private bool visualizing;


	void Update() 
	{
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
			
			float normalized = (value/maxValue);			
			bar.normalizedValue = normalized;
	
	
			t.text = Mathf.RoundToInt(value) + " / " + maxValue;
	}

	public void Visualize(float f) 
	{
			timer = 0;
			if(value != f) 
			{
				//Increase
				amountToFill = f - value;

				fillUpTime = Mathf.Abs((amountToFill / 10));

			}else 
			{
				return;
			}
			visualizing = true;
	}

}
