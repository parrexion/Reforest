using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour {

	public float value;
	public float maxValue;
	public Slider bar;
	public Text t;
	public float normalized;

	[Header("Smooth transitions")]
	public float fillSpeed;
	public float realValue;

	void Start() {
		realValue = value;
	}


	void Update()
	{
		if (realValue != value){
			float step = Time.deltaTime * fillSpeed;
			if (value < realValue){
				value = Mathf.Min(value + step,realValue);
			}
			else {
				value = Mathf.Max(value - step,realValue);
			}
		
			normalized = value / maxValue;
			bar.normalizedValue = normalized;
			t.text = Mathf.RoundToInt(value) + " / " + maxValue;
		}
	}

	public void UpdateRealValue(float f) 
	{
		realValue = f;
	}

	public void UpdateVisualValue(float f) 
	{
		realValue = f;
		value = f;

		normalized = Mathf.Clamp01(1 - f / maxValue);			
		bar.normalizedValue = normalized;
	}

}
