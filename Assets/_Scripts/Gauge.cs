using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour {

	public float value;
	public float maxValue;
	public Image bar;
	public float width = 250;
	public Text t;
	
	private float initialWidth;

	void Update() 
	{

		Visualize();
	}

	public void Visualize() 
	{
		float percent = value/maxValue;
		bar.rectTransform.sizeDelta = new Vector2(percent * width, bar.rectTransform.sizeDelta.y);	

		t.text = value + " / " + maxValue;

	}

}
